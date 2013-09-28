package com.firebase.androidchat;

import java.util.ArrayList;

import com.firebase.client.Firebase;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;
import android.widget.TextView;
import android.widget.Toast;

public class QuestionActivity extends Activity {

	Bundle b;
	private Firebase fireBaseRef;
	private static final String FIREBASE_URL = "https://bunchbot.firebaseIO.com";
	TextView questionTV;
	RadioGroup answerRadioGroup;
	public static final String TAG = QuestionActivity.class.getSimpleName();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.layout_question);

		fireBaseRef = new Firebase(FIREBASE_URL).child("answer");

		b = getIntent().getExtras();

		getActionBar().setTitle(b.getCharSequence("Course_name"));

		questionTV = (TextView) findViewById(R.id.questionTV);
		questionTV.setText(b.getCharSequence("Course_question"));

		answerRadioGroup = (RadioGroup) findViewById(R.id.radioGroup1);

		ArrayList<String> answerSet = b.getStringArrayList("Course_question_answer");

		RadioButton rb0 = (RadioButton) findViewById(R.id.radio0);
		RadioButton rb1 = (RadioButton) findViewById(R.id.radio1);
		RadioButton rb2 = (RadioButton) findViewById(R.id.radio2);
		RadioButton rb3 = (RadioButton) findViewById(R.id.radio3);

		rb0.setText(answerSet.get(0));
		rb1.setText(answerSet.get(1));
		rb2.setText(answerSet.get(2));
		rb3.setText(answerSet.get(3));

	}

	public void onRadioButtonClicked(View view) {
		// Is the button now checked?
		boolean checked = ((RadioButton) view).isChecked();

		SharedPreferences prefs = getApplication().getSharedPreferences("ChatPrefs", 0);
		String username = prefs.getString("username", null);
		
		// Check which radio button was clicked
		switch (view.getId()) {
		case R.id.radio0:
			if (checked) {
				Toast.makeText(this, "Hello" + ((RadioButton) view).getText(), Toast.LENGTH_SHORT).show();
				Answer ans = new Answer(b.getInt("QuestionID"), b.getString("UserID"), 0);
				fireBaseRef.push().setValue(ans);
			}
			break;
		case R.id.radio1:
			if (checked) {
				Toast.makeText(this, "Hello" + ((RadioButton) view).getText(), Toast.LENGTH_SHORT).show();
				Answer ans = new Answer(201, "username", 1);
				fireBaseRef.push().setValue(ans);
			}
				break;
		case R.id.radio2:
			if (checked) {
				Toast.makeText(this, "Hello" + ((RadioButton) view).getText(), Toast.LENGTH_SHORT).show();
				Answer ans = new Answer(201, "username", 2);
				fireBaseRef.push().setValue(ans);
			}
				break;
		case R.id.radio3:
			if (checked) {
				Toast.makeText(this, "Hello" + ((RadioButton) view).getText(), Toast.LENGTH_SHORT).show();
				Answer ans = new Answer(201, "username", 3);
				fireBaseRef.push().setValue(ans);
			}
				break;
		}
	}
}
