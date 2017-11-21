using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    public enum DosageType
    {
        AccordingToNeed = 0, // ”efter behov”: En dosering der udelukkende gives efter behov. Doseringen kan evt. have en begrænsning på en maksimal dagsdosis.
        Temporary = 1, // ”temporær”: En dosering med en slutdato eller en dosering der ikke er gentaget (elementet NotIterated er anvendt i stedet for IterationInterval). Desuden skal doseringen ikke helt eller delivist gives efter behov.
        Fixed = 2, 	   // ”fast”: En itereret dosering uden slutdato, der ikke helt eller delvist gives efter behov.
        OneTime = 3,   // "engangs”: En dosering med kun en enkelt dosis, der ikke gives efter behov.
        Combined = 4, // ”kombineret”: En dosering der både gives (temporært eller fast) og efter behov.
        Unspecified = 5 // ”ikke angivet”: Anvendes for doseringer oprettet gennem versioner før FMK 1.3 / 1.4, og hvor det ikke er muligt at bestemme typen, dvs. at doseringen er som fritekst eller som ”efter skema i lokalt system”. Der kan ikke oprettes doseringer med typen ”ikke angivet” via FMK 1.3 / 1.4.
    };
}
