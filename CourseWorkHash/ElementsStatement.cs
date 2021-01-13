using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkHash
{
    //Перечисления возможных состояний элементов массива в хеш-таблицах с открытой адрессацией
    public enum ElementStatement
    {
        empty = 0,
        chained,
        occupied
    }
}
