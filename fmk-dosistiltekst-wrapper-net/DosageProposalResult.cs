using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    public class DosageProposalResult
    {
        public string XmlSnippet { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
    }
}


/*
package dk.medicinkortet.fmkdosistiltekstwrapper;

public class DosageProposalResult {
	private final String xmlSnippet;
	private final String shortText;
	private final String longText;
	
	public DosageProposalResult(String xmlSnippet, String shortText, String longText) {
		super();
		this.xmlSnippet = xmlSnippet;
		this.shortText = shortText;
		this.longText = longText;
	}

	public String getXmlSnippet() {
		return xmlSnippet;
	}

	public String getShortText() {
		return shortText;
	}

	public String getLongText() {
		return longText;
	}
}
*/