package com.firebase.androidchat;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;
import android.widget.TextView;
import android.widget.Toast;

public class QuestionActivity extends Activity {

	TextView questionTV;
	RadioGroup answerRadioGroup;
	public static final String TAG = QuestionActivity.class.getSimpleName();
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.layout_question);
		
		Bundle b = getIntent().getExtras();
		
		getActionBar().setTitle(b.getCharSequence("Course_name"));
		
		questionTV = (TextView) findViewById(R.id.questionTV);
		questionTV.setText(b.getCharSequence("Course_question"));
		
		answerRadioGroup = (RadioGroup) findViewById(R.id.radioGroup1);
//		answerRadioGroup.get
		
		
		
	}
	
	public void onRadioButtonClicked(View view) {
	    // Is the button now checked?
	    boolean checked = ((RadioButton) view).isChecked();
	    
	    // Check which radio button was clicked
	    switch(view.getId()) {
	        case R.id.radio0:
	            if (checked)
	                Toast.makeText(this, "Hello" + ((RadioButton) view).getText(), Toast.LENGTH_SHORT).show();
	            break;
	        case R.id.radio1:
	            if (checked)
	                // Ninjas rule
	            break;
	        case R.id.radio2:
	            if (checked)
	                // Pirates are the best
	            break;
	        case R.id.radio3:
	            if (checked)
	                // Pirates are the best
	            break;
	    }
	}
}
