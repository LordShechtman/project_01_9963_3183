using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BE
{
   public static class Be_Extensions
    {
        public static Tester ToTester(this XElement stu)
        {
            //Convert Xelmnt object to Tester object
            Tester temp = new Tester();

            temp.Id = stu.Element("id").Value;
            temp.Name = stu.Element("name").Element("firstName").Value;
            temp.FamilyName = stu.Element("name").Element("lastName").Value;
            temp.BirthDate = DateTime.Parse(stu.Element("BIRTH_DAY").Value);
            temp.MyAddress = Configuration.MAKE_Address(stu.Element("Address").Element("City").Value, stu.Element("Address").Element("Street").Value, int.Parse(stu.Element("Address").Element("Number").Value));

            temp.MyGender = Configuration.make_gender(stu.Element("Gender").Value);
            temp.ExpiranceCar = Configuration.MAKE_TYPE_CAR(stu.Element("Type_car").Value);
            temp.YearsOfExperience = int.Parse(stu.Element("seniority").Value);
            temp.MaxTestsPerWeek = int.Parse(stu.Element("MaxTestsPerWeek").Value);
            temp.MaxDistance = int.Parse(stu.Element("MaxDistance").Value);
            temp.PhoneNumber = stu.Element("PhoneNumber").Value;
            temp.hours_string = stu.Element("hours").Value;
            temp.Password = stu.Element("Password").Value;
            temp.set_hours_s(temp.hours_string);
            temp.hours_string = "";
            return temp;
        }
    }
}
