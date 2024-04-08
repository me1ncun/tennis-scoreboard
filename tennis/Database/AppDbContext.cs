using Microsoft.Data.SqlClient;

public class AppDbContext
{
    public static SqlConnection CreateConnection()
    {
        return new SqlConnection("Data Source=.;Initial Catalog=TennisScoreboard;Integrated Security=true;Trust Server Certificate=true;");
    }
}