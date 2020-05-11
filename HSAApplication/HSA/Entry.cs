using System;
using System.Globalization;
using System.Text;

namespace HSA
{
    public class Entry
    {
        //variabiles to hold all the enftry info 
        private string firstName;
        private string lastName;
        private DateTime dob;
        private string planType;
        private DateTime effectiveDate;
        private string status;

        //Constructor
        //firstName - first name of the person
        //lastName - last name of the person
        //dob - date of birth
        //planType - type of plan (HSA,HRA or FSA)
        //effectiveDate - Date when the plan takes effect

        // All parameters are required. I took that as null and "" should throw and exception if they are passed in.
        //The constructor checks to see if the parameters are valid and if they are the set the corresponding variable
        //otherwise and exception is thrown. Im also allowing the first and last name to be really anything beside "" or null.
        //There were no specifications about it and in my eyes a name can be anything it wants to be even "racecar123"

        //We could also make the expceptions being thrown more specfic but that wasn't the requirement was just to return
        //the validation error message
        public Entry(string firstName, string lastName, string dob, string planType, string effectiveDate)
        {
            this.firstName = ((firstName != "") && (firstName != null)) ? firstName : throw new Exception();
            this.lastName = ((lastName != "") && (lastName != null)) ? lastName : throw new Exception();
            this.dob = (DateTime)(checkDob(dob) ?? throw new Exception());
            this.planType = (planType == "HSA" || planType == "HRA" || planType == "FSA") ? planType : throw new Exception();
            this.effectiveDate = (DateTime)(checkValidDate(effectiveDate) ?? throw new Exception());
            setStatus();
        }

        //Checks to see if the date entered in is a valid date in the format Month Day Year (MMddyyyy)
        //date - the string represnting the date
        //returns at DateTime object if the date is valid otherwise return null
        private object checkValidDate(string date)
        {
            DateTime dateTime;
            try{ dateTime = DateTime.ParseExact(date, "MMddyyyy", CultureInfo.InvariantCulture); }
            catch (Exception e) { return null; }
            return dateTime;
        }

        //This method makes sure the date of birth is valid. Valid meaning that the person was born before Today. 
        //You could also check to see if the DOB could mean that the person is probably dead but there was no specified age
        //and I didn't know what "valid" entails

        //If its valid return the dateTime object otherwise return null
        private object checkDob(string date)
        {
            try
            {
                DateTime dobDate =  (DateTime)(checkValidDate(date) ?? throw new Exception());
                DateTime today = DateTime.Today;
                if (dobDate >= today)
                    throw new Exception();
                return dobDate;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        //Sets the status of the entry based on two requirements
        // 1.An individual must be at least 18 years of age to enroll
        // 2.The plan Effective Date may not be more than 30 days in the future
        //If either of the rules in item 4 do not match then the record can be stored with a Status of "Rejected"
        //Records that pass the business rules should be stored with a status of "Accepted"

        private void setStatus()
        {
            double daysInAYear = 365;
            double oldEnough = 18;
            double daysInFuture = 30;
            DateTime today = DateTime.Today;
            double dayDifference = (effectiveDate - today).TotalDays;
            double yearDifference = (today-dob).TotalDays / daysInAYear;
            status = ((dayDifference <= daysInFuture) && (yearDifference >= oldEnough)) ? "Accepted" : "Rejected";
        }

        //This overrides the ToString method
        //returns a string representation of an entry in the following format
        //"Status, FirstName, LastName, DOB (Short Date string), PlanType, EffectiveDate  (Short Date string)"
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //I decided to format the dates the same way they were before. I could do it other ways too ex: MM/dd/yyyy or MM-dd-yyyy
            string dobString = dob.ToString("MMddyyyy");
            string effectiveString = effectiveDate.ToString("MMddyyyy");
            stringBuilder.AppendFormat("{0},{1},{2},{3},{4},{5}", status, firstName, lastName, dobString, planType, effectiveString);
            return stringBuilder.ToString();
        }
    }
}
