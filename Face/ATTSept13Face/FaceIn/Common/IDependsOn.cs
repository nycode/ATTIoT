
namespace FaceIn.Common
{
    
    internal interface IDependsOn<T>
    {
       
        void Inject(T type);
    }
}
