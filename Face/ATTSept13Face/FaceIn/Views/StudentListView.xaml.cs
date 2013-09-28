using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FaceIn.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FaceIn.Views
{

    public partial class StudentListView : UserControl
    { 


        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;

        public StudentListView()
        {
            this.InitializeComponent();
            StudentLoader.Load();

            this.Load();
        }

        

        StudentListModel checkedinStudents = new StudentListModel();

        private void Load()
        {
            var starupPath = Environment.CurrentDirectory;

            face = new HaarCascade("haarcascade_frontalface_default.xml");

            grabber = new Capture(0);
            grabber.QueryFrame();
             
            //StudentLoader.LoadImages();

            //if (StudentLoader.Students.Count <= 0)
            //{
            //    MessageBox.Show("No students found in database. Please add students.", "Face In", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}

            //foreach (var item in StudentLoader.Students)
            //{
            //    var LoadFaces = "sface" + item.Id + ".bmp";
            //    trainingImages.Add(new Image<Gray, byte>(starupPath + "/TrainedFaces/" + LoadFaces));
            //}
             

            //eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                string Labelsinfo = File.ReadAllText(starupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;
                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(starupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No students found in database. Please add students.", "Face In", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
        }

        void FrameGrabber(object sender, EventArgs e)
        { 

            NamePersons.Add("");

            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          face,
          1.2,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new System.Drawing.Size(20, 20));

            Console.WriteLine(facesDetected[0].Length);

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                currentFrame.Draw(f.rect, new Bgr(System.Drawing.Color.Red), 2);

                if (trainingImages.ToArray().Length != 0)
                {
                    //TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                      trainingImages.ToArray(),
                      labels.ToArray(),
                      3000,
                      ref termCrit);

                    name = recognizer.Recognize(result);

                    //Draw the label for each face detected and recognized
                    currentFrame.Draw(name, ref font, new System.Drawing.Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(System.Drawing.Color.LightGreen));
                }

                NamePersons[t - 1] = name;
                NamePersons.Add("");

                label3.Text = facesDetected[0].Length.ToString() + " faces ";

            }
            t = 0;


            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
                var id = (nnn + 1).ToString();

                label4.Text = "Indentified Student " + NamePersons[nnn];

                dispatcherTimer.Stop();

                if (!checkedinStudents.Exists(f => f.Id == id))
                {
                    checkedinStudents.Add(StudentLoader.Students.Find(f => f.Id == id));
                    lstView.ItemsSource = checkedinStudents;
                }
            }
            imageBoxFrameGrabber.Source = ConvertImage(currentFrame);
            
            
            

     
            NamePersons.Clear();

        }

        public BitmapImage ConvertImage(Image<Bgr, Byte> frame)
        {
            MemoryStream ms = new MemoryStream();
            frame.ToBitmap().Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void btnCheckin_Click(object sender, RoutedEventArgs e)
        {

            
            dispatcherTimer.Tick += new EventHandler(FrameGrabber);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new System.Drawing.Size(20, 20));

                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                //labels.Add(textBox1.Text);


                ImageSourceConverter c = new ImageSourceConverter();
                ImageSource srcImage = c.ConvertFrom(TrainedFace.ToBitmap()) as ImageSource;
                //Show face added in gray scale
                imageBox1.Source = srcImage;

                var startupPath = Environment.CurrentDirectory;
                //Write the number of triained faces in a file text for further load
                File.WriteAllText(startupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                //Write the labels of triained faces in a file text for further load
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(startupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(startupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                }

               // MessageBox.Show(textBox1.Text + "´s face detected and added :)", "Training OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new System.Drawing.Size(20, 20));

                if (facesDetected == null || facesDetected.Length <= 0)
                {
                    MessageBox.Show("Not able detect any face, try again");
                    dispatcherTimer.Start();
                }

                //Action for each element detected
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    //draw the face detected in the 0th (gray) channel with blue color
                    currentFrame.Draw(f.rect, new Bgr(System.Drawing.Color.Red), 2);
                    dispatcherTimer.Stop();
                    break;
                }



                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                //labels.Add(textBox1.Text);


                ImageSourceConverter c = new ImageSourceConverter();
                ImageSource srcImage = c.ConvertFrom(TrainedFace.ToBitmap()) as ImageSource;
                //Show face added in gray scale
                imageBox1.Source = srcImage;

                var startupPath = Environment.CurrentDirectory;
                //Write the number of triained faces in a file text for further load
                File.WriteAllText(startupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                //Write the labels of triained faces in a file text for further load
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(startupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(startupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                }

               // MessageBox.Show(textBox1.Text + "´s face detected and added :)", "Training OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    }
}
