using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee:ICloneable
    {
        #region Fileds
        public string Id { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public MyEnum.gender MyGender { get; set; }
        public Address MyAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BrithDate { get; set; }
        public MyEnum.carType Car { get; set; }
        public MyEnum.gear MyGear { get; set; }
        public string School { get; set;}
        public string TeacherName { get; set; }
        public int NumberOfLessons { get; set; }
        #endregion
        //C-TOR
        public Trainee(string id, string name, string familyName, DateTime birthD, MyEnum.gender g, string phoneNum, Address address, MyEnum.carType type, MyEnum.gear my_gear,string school,string teacher_name,int numLessons)
        {
            Id = id;
            Name = name;
            FamilyName = familyName;
            BrithDate = birthD;
            MyGender = g;
            PhoneNumber = phoneNum;
            MyAddress = address;
            Car = type;
            MyGear = my_gear;
            School = school;
            TeacherName = teacher_name;
            NumberOfLessons = numLessons;
        }
        public string showAdress(Address a)
        {
            return a.streetName + " " + a.houseNumber + " " + a.city+"\n";
        }
        
        public override string ToString()
        {
            return "ID:" + this.Id + "\n" +"Name: "+this.Name+" "+this.FamilyName+"\n"+"Address: "+showAdress(MyAddress)+"Phone number: "+this.PhoneNumber+"\n"+"Gender: "
                +this.MyGender+"\n"+"Birth date: "+this.BrithDate+"\n"+"Gear: "+this.MyGear+"\n"+"School: "+this.School+"\n"+"Teacher: "+this.TeacherName+"\n"+"Number of lessons: "+this.NumberOfLessons+"\n"+"Car: "+this.Car;
        }

        public object Clone()
        {
            return new Trainee(Id, Name, FamilyName, BrithDate, MyGender, PhoneNumber, MyAddress, Car, MyGear, School, TeacherName, NumberOfLessons);
        }
    }
}
