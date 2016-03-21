using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace EFTraining.Database
{
    public class Builder
    {
        private static SQLiteConnection _connection = null;

        public static void CreatingDatabase()
        {
            CreateDatabaseTables();
            ImportDatabaseDump();
        }

        private static string StringConnection { get { return "Data Source=:memory:;Version=3;"; } }

        public static SQLiteConnection Connection
        {
            get
            {

                if (_connection == null)
                {
                    _connection = new SQLiteConnection(StringConnection);
                    _connection.Open();
                }

                return _connection;
            }
        }

        private static void CreateDatabaseTables()
        {
            ExecuteCommand(@"
                                            CREATE TABLE Cliente (
	                                            ID integer PRIMARY KEY AUTOINCREMENT,
	                                            Name varchar
                                            );

                                            CREATE TABLE Material (
	                                            ID integer PRIMARY KEY AUTOINCREMENT,
	                                            Name varchar
                                            );

                                            CREATE TABLE Fornecedor (
	                                            ID integer PRIMARY KEY AUTOINCREMENT,
	                                            Name varchar
                                            );
                                            CREATE TABLE Pedido (
	                                                    ID integer PRIMARY KEY AUTOINCREMENT,
	                                                    Date datetime,
	                                                    Cliente_ID integer,
	                                                    Fornecedor_ID integer,
                                                        FOREIGN KEY(Cliente_ID) REFERENCES Cliente(ID),
                                                        FOREIGN KEY(Fornecedor_ID) REFERENCES Fornecedor(ID)
                                                    );

                                                    CREATE TABLE Item_Pedido (
	                                                    ID integer PRIMARY KEY AUTOINCREMENT,
	                                                    Pedido_id integer,
	                                                    Material_ID integer,
	                                                    Qtd integer,
	                                                    Value decimal,
                                                        FOREIGN KEY(Pedido_id) REFERENCES Pedido(ID),
                                                        FOREIGN KEY(Material_ID) REFERENCES Material(ID)
                                                        
                                                    );

                           ");

          
        }

        private static void ImportDatabaseDump()
        {
            //Cliente
            ExecuteCommand(@"
                                Insert into Cliente (Name) values ('John Carter');
                                Insert into Cliente (Name) values ('Tony Stark');
                            "
                            );

            //Materiais
            ExecuteCommand(@"
                                Insert into Material (Name) values ('Pencil');
                                Insert into Material (Name) values ('Eraser');
                            "
                            );

            //Fornecedores
            ExecuteCommand(@"
                                Insert into Fornecedor (Name) values ('Bic');
                                Insert into Fornecedor (Name) values ('Faber Castell');
                            "
                            );

            //Pedido
            ExecuteCommand(@"
                                Insert into Pedido (Date,Cliente_ID,Fornecedor_ID) values ('2016-03-20',1,2);
                                Insert into Pedido (Date,Cliente_ID,Fornecedor_ID) values ('2016-03-21',1,1);
                                Insert into Pedido (Date,Cliente_ID,Fornecedor_ID) values ('2016-03-22',2,1);
                            "
                            );

            //Itens do Pedido do Pedido 1
            ExecuteCommand(@"
                                Insert into Item_Pedido (Pedido_id,Material_ID,Qtd,Value) values (1,1,5,10.5);
                                Insert into Item_Pedido (Pedido_id,Material_ID,Qtd,Value) values (1,2,10,15.5);
                            "
                            );

            //Itens do Pedido do Pedido 2
            ExecuteCommand(@"
                                Insert into Item_Pedido (Pedido_id,Material_ID,Qtd,Value) values (2,2,2,10.5);
                            "
                            );

            //Pedido 3 não possui itens para fins de Dojo

        }

        private static bool ExecuteCommand(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, Connection);

            int affectedRows = command.ExecuteNonQuery();

            return (affectedRows > 0);
        }
    }
}
