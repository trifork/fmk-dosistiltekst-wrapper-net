using fmk_dosistiltekst_wrapper_net.vowrapper;
using Jint;
using Jint.Native;
using Jint.Native.Object;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    public enum FMKVersion
    {
        FMK140,
        FMK142,
        FMK144,
        FMK146
    };

    public class DosisTilTekstWrapper
    {
        const int DosageProposalVersionNo = 1;

        private static ObjectInstance dosisTilTekstJS = null;
        private static JsValue generateXMLSnippetFunc = null;
        private static JsValue shortTextConverterFunc = null;
        private static JsValue shortTextConverter = null;
        private static JsValue longTextConverter = null;
        private static JsValue longTextConverterFunc = null;
        private static Engine engine = null;

        public static void Initialize(StreamReader reader)
        {
            engine = new Engine()
                .SetValue("log", new Action<object>(
                    str =>
                    Console.WriteLine(str)));

            dosisTilTekstJS = engine.Execute(reader.ReadToEnd()).GetValue("dosistiltekst").AsObject();
            if (dosisTilTekstJS == null)
            {
                throw new InvalidDataException("JS object 'dosistiltekst' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }
            generateXMLSnippetFunc = dosisTilTekstJS.Get("DosageProposalXMLGenerator").AsObject().Get("generateXMLSnippet");
            if(generateXMLSnippetFunc == null)
            {
                throw new InvalidDataException("JS function 'generateXMLSnippet' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }
            var factory = dosisTilTekstJS.Get("Factory");
            if (factory == null || factory.IsNull())
            {
                throw new InvalidDataException("JS object 'factory' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }
            shortTextConverter = dosisTilTekstJS.Get("ShortTextConverter").AsObject().Get("getInstance").Invoke();
            if (shortTextConverter == null || shortTextConverter.IsNull())
            {
                throw new InvalidDataException("JS object 'getShortTextConverter' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            shortTextConverterFunc = shortTextConverter.AsObject().Get("convertStr"); 
            if (shortTextConverterFunc == null || shortTextConverterFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'convertStr' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new[] { new DateSerializer() },
                
            };
        }

        public static DosageProposalResult GetDosageProposalResult(string type, string iteration, string mapping, string unitTextSingular, string unitTextPlural, string supplementaryText, IEnumerable<DateTime> beginDates, IEnumerable<DateTime> endDates, FMKVersion version, int dosageProposalVersion)
        {
            if(generateXMLSnippetFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            var result = generateXMLSnippetFunc.Invoke(type, iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, 
                    engine.Array.Construct(beginDates.Select(d => (JsValue)engine.Date.Construct(d)).ToArray()), 
                    engine.Array.Construct(endDates.Select(d => (JsValue)engine.Date.Construct(d)).ToArray()), 
                    version.ToString(), 
                    DosageProposalVersionNo)
                .AsObject();

            
            return new DosageProposalResult()
            {
                LongText = result.Get("_longDosageTranslation").IsString() ? result.Get("_longDosageTranslation").AsString() : null,
                ShortText = result.Get("_shortDosageTranslation").IsString() ? result.Get("_shortDosageTranslation").AsString() : null,
                XmlSnippet = result.Get("_xml").IsString() ? result.Get("_xml").AsString() : null
            };
        }

        public static String ConvertLongText(DosageWrapper dosage)
        {

            if (longTextConverterFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            
            
            json = JsonConvert.SerializeObject(dosage);
            var res = longTextConverterFunc.Invoke(json);


            return res.AsString();
        }

        public static String ConvertShortText(DosageWrapper dosage)
        {
            if (shortTextConverterFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = shortTextConverterFunc.Invoke(shortTextConverter, new[] { new JsValue(json) });

            return res.AsString();
        }
    }
}
