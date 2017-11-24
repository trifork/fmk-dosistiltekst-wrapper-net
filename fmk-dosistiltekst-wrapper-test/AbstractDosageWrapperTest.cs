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

    public abstract class AbstractDosageWrapperTest : DosisTilTekstWrapper
    {
    
        [SetUp]
        public void Init()
        {
            
            // Jenkins path
            var filename = TestContext.CurrentContext.WorkDirectory + "\\..\\..\\fmk-dosis-til-tekst-ts\\workspace\\target\\dosistiltekst.js";
            if (!File.Exists(filename))
            {
                
                // Local devel path
                filename = "\\projects\\fmk-dosis-til-tekst-ts\\target\\dosistiltekst.js";
            }
            DosisTilTekstWrapper.Initialize(File.OpenText(filename));
        }

    }
}
