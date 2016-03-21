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



            Builder.Connection.Close();

        }
    }
}
