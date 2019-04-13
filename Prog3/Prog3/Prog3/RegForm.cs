//A7793
// Program 3
// CIS 199-01
// Due: 11/9/2017
// 

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2018 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }


        const string TIME1 = "8:30 AM";  // 1st time block
        const string TIME2 = "10:00 AM"; // 2nd time block
        const string TIME3 = "11:30 AM"; // 3rd time block
        const string TIME4 = "2:00 PM";  // 4th time block
        const string TIME5 = "4:00 PM";  // 5th time block



        private string JuniorsAndSeniors(char name) //Method for Juniors and Seniors will return a string value
        {
            char[] letter = { 'A', 'E', 'J', 'P', 'T' }; //lower limits for the char of last name first initial
            string[] time = { TIME2, TIME3, TIME4, TIME5, TIME1 };//array for times for registration
            bool found = false; //bool value that allows a user to know if the value is found
            string regTime = "0"; //for bad input


            int index = letter.Length - 1; //starts the array from the end

            while (index >= 0 && !found) //loop- if the index is greater than or equal to 0 and value is found to be true
            {
                if (name >= letter[index])
                    found = true; 
                else
                    --index; //if it is not found in the while loop, it will be decremented by 1

            }

            if (found) 
                regTime = time[index]; //for if the array finds a match it will assign a value while looking parallel

            return regTime; //returns the value when called
        }

        private string SophomoreAndFreshman(char name) //method for sophomore and freshmen times
        {

            char[] letter = { 'A', 'C', 'E', 'G', 'J', 'M', 'P', 'R', 'T', 'W' }; //lower limits for the fresh. soph. last name initials
            string[] time = { TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2 };// times that line up with the letters


            bool found = false; //bool set to false 
            string regTime = "0"; //for bad input


            int index = letter.Length - 1; //starts array from the end

            while (index >= 0 && !found) //loop will run while the index is greater than 0 and found is true
            {
                if (name >= letter[index])
                    found = true; //bool changed to true if input is greater or equal to the char value in the array
                else
                    --index; //index is decremented by 1

            }

            if (found)
                regTime = time[index];

            return regTime; //returns the value if called 


        }

    }
}
            private void findRegTimeBtn_Click(object sender, EventArgs e)
            {
                const string DAY1 = "November 3";   // 1st day of registration
                const string DAY2 = "November 6";   // 2nd day of registration
                const string DAY3 = "November 7";   // 3rd day of registration
                const string DAY4 = "November 8";   // 4th day of registration
                const string DAY5 = "November 9";   // 5th day of registration
                const string DAY6 = "November 10";  // 6th day of registration

              string lastNameStr;       // Entered last name
              char lastNameLetterCh;    // First letter of last name, as char
              string dateStr = "Error"; // Holds date of registration
              string timeStr = "Error"; // Holds time of registration
              bool isUpperClass;        // Upperclass or not?

    
    
          if(char.TryParse(lastNameStr.Text, out lastNameLetterCh ) && lastNameLetterCh >= 0)
    { 
        //Right here is where I ran into problems, I couldn't figure out how to get the last part to work. 
        //I'm not sure what I'm doing wrong and it caused my whole program to crash resulting in the form to be wiped out


         
            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                timeStr = JuniorsAndSeniors(lastNameLetterCh); //This calls the method above for Seniors
                        
                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }

                timeStr = SophomoreAndFreshman(lastNameLetterCh); //calls method for the freshmen and sophomore times  
                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
