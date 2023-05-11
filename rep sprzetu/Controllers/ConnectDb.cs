using Oracle.ManagedDataAccess.Client;
using rep_sprzetu.Models;
using System.Linq;

namespace rep_sprzetu.Controllers
{
    public class ConnectDb
    {
        OracleConnection con;
        public void CreateDbConnection()
        {

            con = new OracleConnection();
            con.ConnectionString = "Data Source=localhost:1521/xe;User Id=C##USER_#;Password=ZAQ12wsxCDE!";
            con.Open();
        }
        public List<HardwareViewModel> AllHardware()
        {
            string query = "SELECT id,Name,Description,tag,owner,dateofassigment,dateofpurchase,previousowner,filia,serwistag,idofhardware,dateofproduction FROM HARDWARE";
            var cmd = new OracleCommand(query, con);
            var reader = cmd.ExecuteReader();
            var listOfHardware = new List<HardwareViewModel>();
            //#TODO: obsłużyć wyjątek gdzie tabela jest pusta
            while (reader.Read())
            {
                var hardware = new HardwareViewModel   
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Tag = reader.GetString(3)?.Split(';').ToList(),
                    Owner = reader.GetString(4),
                    DateOfAssigment = reader.GetDateTime(5),
                    DateOfPurchase = reader.GetDateTime(6),
                    PreviousOwner = reader.GetString(7),
                    Filia = reader.GetString(8),
                    SerwisTag = reader.GetString(9),
                    IdOfHardware = reader.GetString(10),
                    DateOfProduction = reader.GetInt32(11)

                };
                listOfHardware.Add(hardware);
            }
            return listOfHardware;

        }
        public void Close()
        {
            con.Close();
            con.Dispose();
        }
        public void NewRowInDb(HardwareViewModel hardware)
        {
            CreateDbConnection();
            var test = hardware.DateOfAssigment.ToString("mm/dd/yyyy");
            var test2 = hardware.DateOfAssigment.ToString("MM/dd/yyyy");
            var test3 = hardware.DateOfAssigment.ToString("dd/MM/yyyy");
            //string query = @$"INSERT INTO hardware (name, description, tag, owner, dateofassigment, dateofpurchase, previousowner, filia, serwistag, idofhardware,dateofproduction)
            //VALUES ('{hardware.Name}','{hardware.Description}','{hardware.Tag.ToString()}','{hardware.Owner}','{hardware.DateOfAssigment.ToString("MM/dd/yyyy")}','{hardware.DateOfPurchase.ToString("MM/dd/yyyy")}','{hardware.PreviousOwner}','{hardware.Filia}','{hardware.SerwisTag}','{hardware.IdOfHardware}','{hardware.DateOfProduction}')";
            string query = @$"INSERT INTO hardware (name, description, tag, owner, dateofassigment, dateofpurchase, previousowner, filia, serwistag, idofhardware,dateofproduction)
            VALUES ('{hardware.Name}','{string.Join(";",hardware.Tag)}','{hardware.Owner}','{hardware.DateOfAssigment.ToString("yyyyMMdd")}','{hardware.DateOfPurchase.ToString("yyyyMMdd")}','{hardware.PreviousOwner}','{hardware.Filia}','{hardware.SerwisTag}','{hardware.IdOfHardware}','{hardware.DateOfProduction}')";
            var cmd = new OracleCommand(query, con);
            var reader = cmd.ExecuteNonQuery(); 
            //if (reader.)
            //{
            //    //Sukces
            //}
            //else
            //{
            //    //Nie udało się zrobić inserta.
            //}

        }

    }
   
}
