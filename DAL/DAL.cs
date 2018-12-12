using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
public interface Idal
{
    /// <summary>
    /// Tester functions (add,delete and updtae)
    /// </summary>
     void AddTester(Tester t);
    void DeleteTester(string id);
    void UpdateTester(Tester t);
    //----------------------------------
    /// Trinnee functions
    /// 
    void AddTrainee(Trainee t);
    void DeleteTrainee(string id);
    void UpdateTrainee(Trainee t);
    //--------------------------------
    /// TEST Function
    /// 
    void AddTest(Test t);
    void UpdateTest(Test t);

    //--------------geting Functions
    List<Tester> GetAllTesters();
    List<Trainee> GetAllTrainees();
    List<Test> GetAllTests();

}
namespace DAL
{
    
}
