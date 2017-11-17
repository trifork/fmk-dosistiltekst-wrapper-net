using fmk_dosistiltekst_wrapper_net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_test
{
    
    public abstract class AbstractDosageWrapperTest
    {
    
        [SetUp]
        public void Init()
        {
            
            var filename = TestContext.CurrentContext.WorkDirectory + "\\dosistiltekst.js";
            if (!File.Exists(filename))
            {
                filename = "c:\\Projects\\fmk-dosistiltekst-wrapper-net\\fmk-dosistiltekst-wrapper-test\\dosistiltekst.js";
            }
            DosisTilTekstWrapper.Initialize(File.OpenText(filename));
        }

    }
}
