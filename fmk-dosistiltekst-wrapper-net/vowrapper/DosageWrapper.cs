using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class DosageWrapper
    {

        // Wrapped values
        public AdministrationAccordingToSchemaWrapper AdministrationAccordingToSchema { get; protected set; }
        public FreeTextWrapper FreeText { get; protected set; }
        public StructuresWrapper Structures { get; protected set; }

        public static DosageWrapper MakeDosage(StructuresWrapper structures)
        {
            return new DosageWrapper(null, null, structures);
        }

        public static DosageWrapper MakeDosage(FreeTextWrapper freeText)
        {
            return new DosageWrapper(null, freeText, null);
        }

        public static DosageWrapper MakeDosage(AdministrationAccordingToSchemaWrapper administrationAccordingToSchema)
        {
            return new DosageWrapper(administrationAccordingToSchema, null, null);
        }

        private DosageWrapper(AdministrationAccordingToSchemaWrapper administrationAccordingToSchema, FreeTextWrapper freeText, StructuresWrapper structures)
        {
            this.AdministrationAccordingToSchema = administrationAccordingToSchema;
            this.FreeText = freeText;
            this.Structures = structures;
        }
    }
}
