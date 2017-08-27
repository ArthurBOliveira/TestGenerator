using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    class Project
    {
        private string name;
        private List<ClassTo> classTo;

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public List<ClassTo> ClassTo
        {
            get
            {
                return classTo;
            }

            set
            {
                classTo = value;
            }
        }
        #endregion

        #region Constructors
        public Project()
        {
            classTo = new List<ClassTo>();
        }
        #endregion
    }
}
