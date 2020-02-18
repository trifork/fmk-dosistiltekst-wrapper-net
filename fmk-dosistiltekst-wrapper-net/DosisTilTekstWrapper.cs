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
        private static JsValue shortTextConverterClassNameFunc = null;
        private static JsValue shortTextConverter = null;
        private static JsValue longTextConverter = null;
        private static JsValue longTextConverterFunc = null;
        private static JsValue longTextConverterClassNameFunc = null;
        private static JsValue dailyDosisCalculator = null;
        private static JsValue dosageTypeFunc = null;
        private static JsValue dosageType144Func = null;
        private static JsValue combinedTextConverterFunc = null;
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
          
            shortTextConverter = dosisTilTekstJS.Get("ShortTextConverter").AsObject().Get("getInstance").Invoke();
            if (shortTextConverter == null || shortTextConverter.IsNull())
            {
                throw new InvalidDataException("JS object 'shortTextConverter' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            shortTextConverterFunc = shortTextConverter.AsObject().Get("convertStr"); 
            if (shortTextConverterFunc == null || shortTextConverterFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'shortTextConverter.convertStr' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            shortTextConverterClassNameFunc = shortTextConverter.AsObject().Get("getConverterClassNameStr");
            if (shortTextConverterClassNameFunc == null || shortTextConverterClassNameFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'shortTextConverter.getConverterClassName' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            longTextConverter = dosisTilTekstJS.Get("LongTextConverter").AsObject().Get("getInstance").Invoke();
            if (longTextConverter == null || longTextConverter.IsNull())
            {
                throw new InvalidDataException("JS object 'longTextConverter' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            longTextConverterFunc = longTextConverter.AsObject().Get("convertStr");
            if (longTextConverterFunc == null || longTextConverterFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'longTextConverter.convertStr' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            longTextConverterClassNameFunc = longTextConverter.AsObject().Get("getConverterClassNameStr");
            if (longTextConverterClassNameFunc == null || longTextConverterClassNameFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'longTextConverter.getConverterClassName' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            dailyDosisCalculator = dosisTilTekstJS.Get("DailyDosisCalculator").AsObject().Get("calculateStr");
            if (dailyDosisCalculator == null || dailyDosisCalculator.IsNull())
            {
                throw new InvalidDataException("JS object 'dailyDosisCalculator' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            dosageTypeFunc = dosisTilTekstJS.Get("DosageTypeCalculator").AsObject().Get("calculateStr");
            if (dosageTypeFunc == null || dosageTypeFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'DosageTypeCalculator' or function 'calculateStr' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            dosageType144Func = dosisTilTekstJS.Get("DosageTypeCalculator144").AsObject().Get("calculateStr");
            if (dosageType144Func == null || dosageType144Func.IsNull())
            {
                throw new InvalidDataException("JS object 'DosageTypeCalculator144' or function 'calculateStr' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }

            combinedTextConverterFunc = dosisTilTekstJS.Get("CombinedTextConverter").AsObject().Get("convertStr");
            if (combinedTextConverterFunc == null || combinedTextConverterFunc.IsNull())
            {
                throw new InvalidDataException("JS object 'CombinedTextConverter' or function 'convertStr' not found. Is the supplied reader streaming a correct dosistiltekst.js file?");
            }


            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        public static DosageProposalResult GetDosageProposalResult(string type, string iteration, string mapping, string unitTextSingular, string unitTextPlural, string supplementaryText, IEnumerable<DateTime> beginDates, IEnumerable<DateTime?> endDates, FMKVersion version, int dosageProposalVersion, int? shortTextMaxLength = null)
        {
            if(generateXMLSnippetFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            ObjectInstance result;
            
            if(shortTextMaxLength.HasValue) {
                result = generateXMLSnippetFunc.Invoke(type, iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, 
                    engine.Array.Construct(beginDates.Select(d => (JsValue)engine.Date.Construct(d)).ToArray()),
                    engine.Array.Construct(endDates.Select(d => (JsValue)(d.HasValue ? engine.Date.Construct(d.Value) : Jint.Native.Undefined.Instance)).ToArray()), 
                    version.ToString(), 
                    DosageProposalVersionNo,
                    shortTextMaxLength.Value)
                .AsObject();
            }
            else 
            {
                result = generateXMLSnippetFunc.Invoke(type, iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, 
                    engine.Array.Construct(beginDates.Select(d => (JsValue)engine.Date.Construct(d)).ToArray()),
                    engine.Array.Construct(endDates.Select(d => (JsValue)(d.HasValue ? engine.Date.Construct(d.Value) : Jint.Native.Undefined.Instance)).ToArray()), 
                    version.ToString(), 
                    DosageProposalVersionNo)
                .AsObject();
            }
             

            
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
            json = JsonConvert.SerializeObject(dosage, new DateSerializer());
            JsValue res = res = longTextConverterFunc.Invoke(longTextConverter, new[] { new JsValue(json) });
            return res.IsNull() ? null : res.AsString();
        }

        public static string ConvertShortText(DosageWrapper dosage)
        {
            if (shortTextConverterFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = shortTextConverterFunc.Invoke(shortTextConverter, new[] { new JsValue(json) });

            return res.IsNull() ? null : res.AsString();
        }

        public static string ConvertShortText(DosageWrapper dosage, int maxLength)
        {
            if (shortTextConverterFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = shortTextConverterFunc.Invoke(shortTextConverter, new[] { new JsValue(json), new JsValue(maxLength) });
            return res.IsNull() ? null : res.AsString();
        }

        /// <summary>
        /// Method to be used by unit-testing only
        /// </summary>
        /// <param name="dosage"></param>
        /// <returns></returns>
        protected static string GetLongTextConverterClassName(DosageWrapper dosage)
        {
            if (longTextConverterClassNameFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = longTextConverterClassNameFunc.Invoke(longTextConverter, new[] { new JsValue(json) });
            return res.IsNull() ? null : res.AsString();
        }

        /// <summary>
        /// Method to be used by unit-testing only
        /// </summary>
        /// <param name="dosage"></param>
        /// <returns></returns>
        protected static string GetShortTextConverterClassName(DosageWrapper dosage)
        {
            if (shortTextConverterClassNameFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = shortTextConverterClassNameFunc.Invoke(shortTextConverter, new[] { new JsValue(json) });
            return res.IsNull() ? null : res.AsString();
        }



        public static DosageType GetDosageType(DosageWrapper dosage)
        {
            if (dosageTypeFunc == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = dosageTypeFunc.Invoke(new JsValue(json));
            if(res.IsNull())
            {
                return DosageType.Unspecified;
            }
            else
            {
                return (DosageType)Enum.Parse(typeof(DosageType), res.AsNumber().ToString());
            }
        }

        public static DosageType GetDosageType144(DosageWrapper dosage)
        {
            if (dosageType144Func == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = dosageType144Func.Invoke(new JsValue(json));
            if (res.IsNull())
            {
                return DosageType.Unspecified;
            }
            else
            {
                return (DosageType)Enum.Parse(typeof(DosageType), res.AsNumber().ToString());
            }
        }

        public static DosageTranslationCombined ConvertCombined(DosageWrapper dosage) {
		
		    if(combinedTextConverterFunc == null) {
			    throw new Exception("DosisTilTekstWrapper not initialized - call initialize() method before invoking any of the methods");
		    }
				
		    String json = "(unset)";
		    json = JsonConvert.SerializeObject(dosage);
             var res = combinedTextConverterFunc.Invoke(new JsValue(json));

		
		    if(res.IsNull()) {
			    return new DosageTranslationCombined(new DosageTranslation(null, null, new DailyDosis()), new List<DosageTranslation>());
		    }

            var combinedShortText = res.AsObject().Get("combinedShortText").IsNull() ? null : res.AsObject().Get("combinedShortText").AsString();
		    var combinedLongText = res.AsObject().Get("combinedLongText").AsString();
		
		    var combinedDD = GetDailyDosisFromJS(res.AsObject().Get("combinedDailyDosis").AsObject());
		    var  periodTextArray = res.AsObject().Get("periodTexts").AsArray();
		    var translations = new List<DosageTranslation>();
            for (int i = 0; i < periodTextArray.GetLength(); i++)
            {
                var periodTexts = periodTextArray.Get(i.ToString()).AsArray();
                String periodText1 = null;
                String periodText2 = null;
                if (periodTexts.Get("0").IsString())
                {
                    periodText1 = periodTexts.Get("0").AsString();
                }
                if (periodTexts.Get("1").IsString())
                {
                    periodText2 = periodTexts.Get("1").AsString();
                }
                translations.Add(new DosageTranslation(periodText1, periodText2, GetDailyDosisFromJS(periodTexts.Get("2").AsObject())));
            }
		    return new DosageTranslationCombined(new DosageTranslation(combinedShortText, combinedLongText, combinedDD), translations);
	}

        public static DailyDosis CalculateDailyDosis(DosageWrapper dosage)
        {
            if (dailyDosisCalculator == null)
            {
                throw new Exception("Initialize must be called before calling other methods");
            }

            string json = "(unset)";
            json = JsonConvert.SerializeObject(dosage);
            var res = dailyDosisCalculator.Invoke(new JsValue(json));
            if(res.IsNull())
            {
                return null;
            }
            else
            {
                return GetDailyDosisFromJS(res.AsObject());
            }
        }

        private static DailyDosis GetDailyDosisFromJS(ObjectInstance dailyDosisObject) {
            var unitOrUnits = dailyDosisObject.Get("unitOrUnits");
		    if(unitOrUnits == null || unitOrUnits.IsNull() || unitOrUnits.IsUndefined()) {
			    return new DailyDosis();
		    }
		    UnitOrUnitsWrapper unitWrapper;
            var unit = unitOrUnits.AsObject().Get("unit");
		    if(unit != null && unit.IsString()) {
			    unitWrapper = UnitOrUnitsWrapper.MakeUnit(unitOrUnits.AsObject().Get("unit").AsString());
		    }
		    else {
			    unitWrapper = UnitOrUnitsWrapper.MakeUnits(unitOrUnits.AsObject().Get("unitSingular").AsString(), unitOrUnits.AsObject().Get("unitPlural").AsString());
		    }
		    var value = dailyDosisObject.Get("value");
		    if(value != null && !value.IsNull() && !value.IsUndefined()) {
            
			    return new DailyDosis(value.AsNumber(), unitWrapper);
		    }
		    else {
			    var interval = dailyDosisObject.Get("interval");
			    return new DailyDosis(new Interval<double>(interval.AsObject().Get("minimum").AsNumber(), interval.AsObject().Get("maximum").AsNumber()), unitWrapper);
		    }
	    }
    }
}
