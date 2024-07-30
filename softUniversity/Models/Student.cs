using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softUniversity.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public int ID { get; set; }

        public bool Gender { get; set; }

        public string GenderName
        {
            get
            {
                if (Gender)
                    return "زن";
                else
                    return "مرد";
            }
        }
        public bool IsMarried { get; set; }

        public string MarriedName
        {
            get
            {
                if (IsMarried)
                    return "متاهل ";
                else
                    return "مجرد";
            }
        }

        public Proof Proof { get; set; }

        public string ProofName
        {
            get
            {
                if (Proof == null)
                    return "بدون مقطع";

                return Proof.ProofName;
            }
        }

    }
}
