using System.Data.SqlClient;

using var con = new SqlConnection("");

//await -> espera a que la tarea termine.
//Task -> objeto que puede existir o existirá en un futuro (es una promesa)
await con.OpenAsync();

//SELECT
using var select = new SqlCommand("Select Id, Name from tablaexample",con);
var reader = await select.ExecuteReaderAsync();

if(reader.HasRows)
{
    while(await reader.ReadAsync())
    {
        Console.WriteLine($"ID ->{(int) reader[0]}");
        Console.WriteLine($"NAME ->{reader[1].ToString()}");
    }
}
else{
    //INSERT
    Console.WriteLine("BBDD vacia. Insertamos!");

    await reader.CloseAsync();

    using var cmd = new SqlCommand("Insert into tablaexample values (1,'Text')",con);
    await cmd.ExecuteNonQueryAsync();
}