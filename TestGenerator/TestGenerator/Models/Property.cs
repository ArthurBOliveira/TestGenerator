namespace TestGenerator.Generators
{
    public class Property
    {
        private string type;
        private string name;
        private bool isKey;
        private bool isObj;

        #region Properties
        public bool IsObj
        {
            get
            {
                return isObj;
            }

            set
            {
                isObj = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

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

        public bool IsKey
        {
            get
            {
                return isKey;
            }

            set
            {
                isKey = value;
            }
        }
        #endregion

        #region Constructor
        public Property() { }
        #endregion
    }
}
