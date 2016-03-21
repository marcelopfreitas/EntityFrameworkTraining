using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFTraining.Database;
namespace EFTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            Builder.CreatingDatabase();

            /*INICIO --Inserir aqui código de query*/

            



            
            
            /*FIM    --Inserir aqui código de query*/

            Builder.Connection.Close();

        }
    }
}
