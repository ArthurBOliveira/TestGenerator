using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestGenerator.Generators
{
    class TestGenerator : Generator
    {
        public static string Generate(ClassTo classTo)
        {
            string text = "";

            text += "using System;\r\n";
            text += "using Microsoft.VisualStudio.TestTools.UnitTesting;\r\n";
            text += "using System.Collections.Generic;\r\n";
            text += "using JobManager_Back.Models;\r\n";
            text += "using JobManager.Controllers;\r\n";
            text += "using System.Web.Http;\r\n";
            text += "using System.Threading.Tasks;\r\n";
            text += "using System.Web.Http.Results;\r\n";
            text += "using System.Net;\r\n\r\n";

            text += "namespace " + classTo.ProjectName + ".IntegrationTest\r\n";
            text += "{\r\n";

            text += "\t[TestClass]\r\n";
            text += "\tpublic class " + classTo.Model.Name + "Repository : BaseTest\r\n";
            text += "\t{\r\n";

            foreach (Method method in classTo.Methods)
            {
                switch (method.Type)
                {
                    case MethodType.GET:
                        text += CreateTestForGet(method, classTo.Name);
                        break;
                    case MethodType.LIST:
                        text += CreateTestForList(method);
                        break;
                    case MethodType.POST:
                        text += CreateTestForPost(method);
                        break;
                    case MethodType.PUT:
                        text += CreateTestForPut(method);
                        break;
                    case MethodType.DELETE:
                        text += CreateTestForDelete(method);
                        break;
                    default:
                        text += "";
                        break;
                }
            }

            text += "\t}\r\n";
            text += "}";

            StreamWriter file = File.AppendText(classTo.ProjectName + "IntegrationTest");

            file.WriteLine(text);

            file.Close();

            return text;
        }

        private static string CreateTestForDelete(Method method)
        {
            //throw new NotImplementedException();
            return "";
        }

        private static string CreateTestForPut(Method method)
        {
            //throw new NotImplementedException();
            return "";
        }

        private static string CreateTestForPost(Method method)
        {
            //throw new NotImplementedException();
            return "";
        }

        private static string CreateTestForList(Method method)
        {
            //throw new NotImplementedException();
            return "";
        }

        private static string CreateTestForGet(Method method, string name)
        {
            string text = "";
            string param = "";

            List<string> extensions = new List<string>();

            text += "\t\t[TestMethod]\r\n";
            text += "\t\tpublic async Task " + method.Name + " _Ok_Test()\r\n";
            text += "\t\t{\r\n";

            foreach(Params p in method.Params)
            {
                int i = 1;

                if (p.IsObj)
                {
                    text += "\t\t\tvar param" + i + " = new " + p.Type + "();\r\n";

                    foreach(Property pr in p.Model.Properties)
                    {
                        //filter.name = "a";
                        string aux = "\t\t\tparam" + i + "." + pr.Name + " = |TODO|;\r\n";

                        extensions.Add(aux);
                    }
                }
                else
                    text += "\t\t\t" + p.Type + " param" + i + ";\r\n";

                param += "param" + i + ", ";
                i++;
            }

            if (extensions.Any())
            {
                foreach (string ext in extensions)
                {
                    int i = 1;

                    text += "\t\t\t//Test " + i + " \r\n";

                    text += ext;

                    text += "\t\t\tIHttpActionResult result = new " + name + "." + method.Name + "(" + param + ");\r\n";

                    text += "\t\t\tAssert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<|TODO|>));\r\n";

                    i++;
                }
            }
            else
            {

                text += "\t\t\tIHttpActionResult result = new " + name + "." + method.Name + "(" + param + ");\r\n";

                text += "\t\t\tAssert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<|TODO|>));\r\n";
            }

            text += "\t\t}\r\n\r\n";

            return text;
        }
    }
}
