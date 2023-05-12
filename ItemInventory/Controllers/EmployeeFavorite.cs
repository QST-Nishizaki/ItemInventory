using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Transactions;
using ItemInventory;

namespace ItemInventory.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EmployeeFavorite : ControllerBase
  {
    //private static readonly string ConnectionString = "Server=127.0.0.1;Port=5432;User ID=postgres;Database=postgres;Password=P@ssw0rd;Enlist=true";
    private readonly ILogger<EmployeeFav> _logger;

    public EmployeeFavorite(ILogger<EmployeeFav> logger)
    {
      _logger = logger;
    }

    public static List<EmployeeFav>? employees = new List<EmployeeFav>();

    [HttpGet]
    public IEnumerable<EmployeeFav> Get()
    {

      using (TransactionScope ts = new TransactionScope())
      {
        /* using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
        {
            conn.Open();

            //. �e�[�u�����Ȃ��Ƃ��͒ǉ�����.
            var sql = "create table if not exists sample_employee (id integer generated always as identity, name varchar(64) )";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            //. �f�[�^���Ƃ��Ă���.
            var dt = new DataTable();
            sql = "select * from sample_employee";
            cmd = new NpgsqlCommand(sql, conn);
            var da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);

            employees = dt.Rows.Cast<DataRow>().Select(r => new ItemInventory.EmployeeFav((int)r[0], (string)r[1])).ToList();

        } */
        ts.Complete();
      }

      if (employees is null)
      {
        //. returns empty
        return new List<EmployeeFav>();
      }

      return employees;
    }

    [HttpPost]
    public ObjectResult Post([FromBody] EmployeeFav favEmployee)
    {
      var result=new ObjectResult("test");
      using (TransactionScope ts = new TransactionScope())
      {
        /* using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
        {
            conn.Open();

            //. �e�[�u�����Ȃ��Ƃ��͒ǉ�����.
            var sql = "create table if not exists sample_employee (id integer generated always as identity, name varchar(64) )";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            //. �f�[�^�x�[�X�ɒǉ�.
            sql = $"insert into sample_employee (name) values ('{newEmployee.name}')";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        } */
        if (employees is not null)
        {
          var find=employees.FindIndex(data => data.id == favEmployee.id);
          if (find != -1)
          {
            employees[find] = new EmployeeFav(favEmployee.id, favEmployee.other_id);
            result = new ObjectResult("updated");
          }
          else
          {
            employees.Add(new EmployeeFav(favEmployee.id, favEmployee.other_id));
            result = new ObjectResult("created");
          }//newEmployee.fav,newEmployee.roulette,newEmployee.checkin
        }
        else
        {
          employees = new List<EmployeeFav>();
        }

        ts.Complete();
      }

      return result;
    }
  }
}