package com.firebase.androidchat;

public class Answer {

	private int questionID;
	private String userID;
	private int option;

    // Required default constructor for Firebase object mapping
    private Answer() { }

    Answer(int qID, String uID, int opt) {
        questionID = qID;
        userID = uID;
        option = opt;
    }

    /**
     * 
     * @return
     */
	public int getQuestionID() {
		return questionID;
	}

	public String getUserID() {
		return userID;
	}

	public int getOption() {
		return option;
	}
    

}
