using fmk_dosistiltekst_wrapper_net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fmk_dosistiltekst_wrapper_net.vowrapper;
using System.Runtime.CompilerServices;

namespace fmk_dosistiltekst_wrapper_test
{

    public abstract class AbstractDosageWrapperTest : DosisTilTekstWrapper
    {
    
        [SetUp]
        public void Init()
        {
            
            
            // Local devel path
            var filename = "\\projects\\fmk-dosis-til-tekst-ts\\target\\dosistiltekst.js";
			//var filename = "/home/markus/FMK/fmk-dosis-til-tekst-ts/target/dosistiltekst.js";

            
            if (!File.Exists(filename))
            {
                // Jenkins path
                filename = TestContext.CurrentContext.WorkDirectory + "/../../../../../../../fmk-dosis-til-tekst-ts/workspace/target/dosistiltekst.js";    
            }
            DosisTilTekstWrapper.Initialize(File.OpenText(filename));
        }

        public void AssertLongTextEquals(DosageWrapper dosage, [CallerFilePathAttribute] string callerFilepath = "",  [CallerMemberName] string callerName = "")
        {
            string testclass = Path.GetFileNameWithoutExtension(callerFilepath);
            var pathElements = callerFilepath.Split(Path.DirectorySeparatorChar).ToList();
            
            string testnamespace = pathElements[pathElements.Count - 2];

            // Local devel path

            var filename = TestContext.CurrentContext.WorkDirectory + "/../../../../../fmk-dosistiltekst-wrapper/teststrings/longtext/" + testnamespace + "/" + testclass + "/" + callerName + ".txt";  

            if (!File.Exists(filename))
            {
                // Jenkins path
                filename = TestContext.CurrentContext.WorkDirectory + "/../../../../../../fmk-dosistiltekst-wrapper/teststrings/longtext/" + testnamespace + "/" + testclass + "/"  + callerName + ".txt";    
            }   
            
            var reader = File.OpenText(filename);
            var expectedResult = reader.ReadToEnd();
            Assert.AreEqual(expectedResult, DosisTilTekstWrapper.ConvertLongText(dosage));
        }
        
    }
}
