var dosistiltekst =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// identity function for calling harmony imports with the correct context
/******/ 	__webpack_require__.i = function(value) { return value; };
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 59);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TextHelper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__vowrapper_DayOfWeek__ = __webpack_require__(47);

var TextHelper = (function () {
    function TextHelper() {
    }
    TextHelper.formatQuantity = function (quantity) {
        // We replace . with , below using string replace as we want to make
        // sure we always use , no matter what the locale settings are
        return TextHelper.trim(quantity.toString().replace(".", ","));
    };
    TextHelper.gange = function (num) {
        if (num === undefined || Math.floor(num) > 1)
            return "gange";
        else
            return "gang";
    };
    TextHelper.maybeAddSpace = function (supplText) {
        if (supplText && (supplText.substr(0, 1) === "," || supplText.substr(0, 1) === "."))
            return "";
        else
            return " ";
    };
    TextHelper.trim = function (numberStr) {
        if (numberStr.indexOf(".") < 0 && numberStr.indexOf(",") < 0)
            return numberStr;
        if (numberStr.length === 1 || numberStr.charAt(numberStr.length - 1) > "0")
            return numberStr;
        else
            return TextHelper.trim(numberStr.substring(0, numberStr.length - 1));
    };
    TextHelper.formatDate = function (date) {
        return date.toISOString().substr(0, 10);
    };
    TextHelper.quantityToString = function (quantity) {
        var quantityString = TextHelper.formatQuantity(quantity);
        var fractions = TextHelper._decimalsToFractions[quantityString];
        if (fractions) {
            return fractions;
        }
        else {
            return quantityString;
        }
    };
    TextHelper.unitToSingular = function (plural) {
        var singular = TextHelper._unit.filter(function (u) { return u[1] === plural; }).map(function (u) { return u[0]; });
        if (singular && singular.length > 0) {
            return singular[0];
        }
        return plural;
    };
    TextHelper.unitToPlural = function (singular) {
        var plural = TextHelper._unit.filter(function (u) { return u[0] === singular; });
        if (plural && plural.length > 0) {
            return plural[0][1];
        }
        return singular;
    };
    TextHelper.getUnit = function (dose, unitOrUnits) {
        if (!unitOrUnits)
            return "";
        if (unitOrUnits.getUnit())
            return TextHelper.correctUnit(dose, unitOrUnits.getUnit());
        else if (unitOrUnits.getUnitSingular() && unitOrUnits.getUnitPlural())
            return TextHelper.chooseUnit(dose, unitOrUnits.getUnitSingular(), unitOrUnits.getUnitPlural());
        else
            return "";
    };
    TextHelper.getUnitFromDoseNumber = function (dose, unitOrUnits) {
        if (unitOrUnits.getUnit())
            return TextHelper.correctUnitForDoseNumber(dose, unitOrUnits.getUnit());
        else if (unitOrUnits.getUnitSingular() && unitOrUnits.getUnitPlural())
            return TextHelper.chooseUnitForDoseNumber(dose, unitOrUnits.getUnitSingular(), unitOrUnits.getUnitPlural());
        else
            return "";
    };
    TextHelper.correctUnit = function (dose, unit) {
        if (TextHelper.hasPluralUnit(dose))
            return TextHelper.unitToPlural(unit);
        else
            return TextHelper.unitToSingular(unit);
    };
    TextHelper.correctUnitForDoseNumber = function (dose, unit) {
        if (TextHelper.hasPluralUnitForNumber(dose))
            return TextHelper.unitToPlural(unit);
        else
            return TextHelper.unitToSingular(unit);
    };
    TextHelper.chooseUnit = function (dose, unitSingular, unitPlural) {
        if (TextHelper.hasPluralUnit(dose))
            return unitPlural;
        else
            return unitSingular;
    };
    TextHelper.chooseUnitForDoseNumber = function (dose, unitSingular, unitPlural) {
        if (TextHelper.hasPluralUnitForNumber(dose))
            return unitPlural;
        else
            return unitSingular;
    };
    TextHelper.hasPluralUnit = function (dose) {
        if (dose.getDoseQuantity() !== undefined) {
            return dose.getDoseQuantity() > 1.0 || dose.getDoseQuantity() < 0.000000001;
        }
        else if (dose.getMaximalDoseQuantity() !== undefined) {
            return dose.getMaximalDoseQuantity() > 1.0 || dose.getMaximalDoseQuantity() < 0.000000001;
        }
        else {
            return false;
        }
    };
    TextHelper.hasPluralUnitForNumber = function (dose) {
        if (dose !== undefined) {
            return dose > 1.0 || dose < 0.000000001;
        }
        else {
            return false;
        }
    };
    TextHelper.makeDayString = function (startDateOrDateTime, dayNumber) {
        var d = TextHelper.makeFromDateOnly(startDateOrDateTime.getDateOrDateTime());
        d.setDate(d.getDate() + dayNumber - 1);
        var dateString = TextHelper.formatLongDate(d);
        return dateString.charAt(0).toUpperCase() + dateString.substr(1);
    };
    TextHelper.makeFromDateOnly = function (date) {
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), 0, 0, 0);
    };
    TextHelper.formatLongDate = function (date) {
        return TextHelper.weekdays[date.getDay()] + " den " + date.getDate() + ". " + TextHelper.months[date.getMonth()] + " " + date.getFullYear();
    };
    TextHelper.formatLongDateTime = function (dateTime) {
        // "EEEEEEE "den" d"." MMMMMMM yyyy "kl." HH:mm:ss";
        return TextHelper.formatLongDate(dateTime) + " kl. " + TextHelper.pad(dateTime.getHours(), 2) + ":" + TextHelper.pad(dateTime.getMinutes(), 2) + ":" + TextHelper.pad(dateTime.getSeconds(), 2);
    };
    TextHelper.formatLongDateNoSecs = function (dateTime) {
        var dateTimeString = TextHelper.formatLongDateTime(dateTime);
        return dateTimeString.substr(0, dateTimeString.length - 3);
    };
    TextHelper.pad = function (n, length) {
        var s = String(n);
        while (s.length < (length || 2)) {
            s = "0" + s;
        }
        return s;
    };
    TextHelper.formatYYYYMMDD = function (d) {
        return TextHelper.pad(d.getFullYear(), 4) + "-" + TextHelper.pad(d.getMonth() + 1, 2) + "-" + TextHelper.pad(d.getDate(), 2);
    };
    TextHelper.makeDayOfWeekAndName = function (startDateOrDateTime, day, initialUpperCase) {
        var dateOnly = TextHelper.makeFromDateOnly(startDateOrDateTime.getDateOrDateTime());
        dateOnly.setDate(dateOnly.getDate() + day.getDayNumber() - 1);
        var dayString = TextHelper.weekdays[dateOnly.getDay()];
        var name;
        if (initialUpperCase)
            name = dayString.charAt(0).toUpperCase() + dayString.substring(1);
        else
            name = dayString;
        return new __WEBPACK_IMPORTED_MODULE_0__vowrapper_DayOfWeek__["a" /* DayOfWeek */](dateOnly.getDay(), name, day);
    };
    TextHelper.strStartsWith = function (str, prefix) {
        return str.indexOf(prefix) === 0;
    };
    TextHelper.strEndsWith = function (str, suffix) {
        return str.lastIndexOf(suffix) === str.length - suffix.length;
    };
    TextHelper.VERSION = "2014-02-10";
    TextHelper.INDENT = "   ";
    //    private static final FastDateFormat longDateTimeFormatter = FastDateFormat.getInstance(LONG_DATE_TIME_FORMAT, new Locale("da", "DK"));
    //    private static final FastDateFormat longDateTimeFormatterNoSecs = FastDateFormat.getInstance(LONG_DATE_TIME_FORMAT_NO_SECS, new Locale("da", "DK"));
    //    private static final FastDateFormat longDateFormatter = FastDateFormat.getInstance(LONG_DATE_FORMAT, new Locale("da", "DK"));
    TextHelper._decimalsToFractions = { "0,5": "1/2", "0,25": "1/4", "0,75": "3/4", "1,5": "1 1/2" };
    TextHelper._unit = [
        ["ampul", "ampuller"],
        ["applikatordosis", "applikatordoser"],
        ["beholder", "beholdere"],
        ["behandling", "behandlinger"],
        ["brev", "breve"],
        ["brusetablet", "brusetabletter"],
        ["bukkalfilm", "bukkalfilm"],
        ["doseringssprøjte", "doseringssprøjter"],
        ["dosis", "doser"],
        ["dråbe", "dråber"],
        ["engangspen", "engangspenne"],
        ["engangssprøjte", "engangssprøjter"],
        ["enkeltdosisbeholder", "enkeltdosisbeholdere"],
        ["hætteglas", "hætteglas"],
        ["hætteglas+brev", "hætteglas+breve"],
        ["IE", "IE"],
        ["implantat", "implantater"],
        ["indgnidning", "indgnidninger"],
        ["indsprøjtning", "indsprøjtninger"],
        ["inhalationskapsel", "inhalationskapsler"],
        ["injektionssprøjte", "injektionssprøjter"],
        ["injektor", "injektorer"],
        ["kapsel", "kapsler"],
        ["kapsel med inhalationspulver", "kapsler med inhalationspulver"],
        ["L", "L"],
        ["liter", "liter"],
        ["mg", "mg"],
        ["ml", "ml"],
        ["måleske", "måleskeer"],
        ["oral sprøjte", "orale sprøjter"],
        ["pensling", "penslinger"],
        ["plaster", "plastre"],
        ["plastflaske", "plastflasker"],
        ["pudring", "pudringer"],
        ["pulver+solvens", "pulver+solvens"],
        ["pump", "pump"],
        ["pust", "pust"],
        ["påsmøring", "påsmøringer"],
        ["rektal stikpille", "rektale stikpiller"],
        ["rektalsprøjte", "rektalsprøjte"],
        ["rektaltube", "rektaltuber"],
        ["resoriblet", "resoribletter"],
        ["skylning", "skylninger"],
        ["spiral", "spiraler"],
        ["streg", "streger"],
        ["stribe", "striber"],
        ["strømpe", "strømper"],
        ["sug", "sug"],
        ["sugetablet", "sugetabletter"],
        ["sæt breve A+B", "sæt breve A+B"],
        ["tablet", "tabletter"],
        ["tablet+opløsningsvæske", "tabletter+opløsningsvæsker"],
        ["tablet til rektal anvendelse", "tabletter til rektal anvendelse"],
        ["tryk", "tryk"],
        ["tube", "tuber"],
        ["tubule", "tubuler"],
        ["tyggegummi", "tyggegummier"],
        ["tyggetabletter", "tyggetabletter"],
        ["tykt lag", "tykke lag"],
        ["tyndt lag", "tynde lag"],
        ["vaginalindlæg", "vaginalindlæg"],
        ["vaginalkapsel", "vaginalkapsler"],
        ["vaginaltablet", "vaginaltabletter"],
        ["vagitorie", "vagitorier"],
        ["vask", "vaske"],
        ["øjenlamel", "øjenlamel"],
        ["øjenskylning", "øjenskylninger"],
    ];
    TextHelper.weekdays = [
        "søndag",
        "mandag",
        "tirsdag",
        "onsdag",
        "torsdag",
        "fredag",
        "lørdag"
    ];
    TextHelper.months = [
        "januar",
        "februar",
        "marts",
        "april",
        "maj",
        "juni",
        "juli",
        "august",
        "september",
        "oktober",
        "november",
        "december"
    ];
    return TextHelper;
}());



/***/ }),
/* 1 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ShortTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__TextHelper__ = __webpack_require__(0);

var ShortTextConverterImpl = (function () {
    function ShortTextConverterImpl() {
    }
    ShortTextConverterImpl.toValue = function (dose) {
        if (dose.getDoseQuantity() !== undefined) {
            return __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].quantityToString(dose.getDoseQuantity());
        }
        else if (dose.getMinimalDoseQuantity() !== undefined && dose.getMaximalDoseQuantity() !== undefined) {
            return __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].quantityToString(dose.getMinimalDoseQuantity()) +
                "-" + __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].quantityToString(dose.getMaximalDoseQuantity());
        }
        else {
            return undefined;
        }
    };
    ShortTextConverterImpl.toQuantityValue = function (dose) {
        if (dose !== undefined) {
            return __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].quantityToString(dose);
        }
        else {
            return undefined;
        }
    };
    ShortTextConverterImpl.hasIntegerValue = function (n) {
        return n % 1 === 0;
    };
    ShortTextConverterImpl.toDoseAndUnitValue = function (dose, unitOrUnits) {
        var s = this.toValue(dose);
        var u = __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].getUnit(dose, unitOrUnits);
        if (dose.getLabel().length === 0)
            return s + " " + u;
        else
            return s + " " + u + " " + dose.getLabel();
    };
    ShortTextConverterImpl.toDoseLabelUnitValue = function (dose, label, unitOrUnits) {
        var s = this.toQuantityValue(dose);
        var u = __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].getUnitFromDoseNumber(dose, unitOrUnits);
        if (label === undefined || label.length === 0)
            return s + " " + u;
        else
            return s + " " + u + " " + label;
    };
    ShortTextConverterImpl.prototype.calculateNumberOfWholeWeeks = function (iterationInterval) {
        var numberOfWholeWeeks = iterationInterval / 7;
        if (numberOfWholeWeeks.toFixed() !== numberOfWholeWeeks.toString())
            numberOfWholeWeeks = -1;
        return numberOfWholeWeeks;
    };
    ShortTextConverterImpl.prototype.calculateNumberOfWholeMonths = function (iterationInterval) {
        var numberOfWholeMonths = iterationInterval / 30;
        if (numberOfWholeMonths.toFixed() !== numberOfWholeMonths.toString())
            numberOfWholeMonths = -1;
        return numberOfWholeMonths;
    };
    return ShortTextConverterImpl;
}());



/***/ }),
/* 2 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosageWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__AdministrationAccordingToSchemaWrapper__ = __webpack_require__(46);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__FreeTextWrapper__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__StructuresWrapper__ = __webpack_require__(7);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__Validator__ = __webpack_require__(32);




var DosageWrapper = (function () {
    function DosageWrapper(administrationAccordingToSchema, freeText, structures) {
        this.administrationAccordingToSchema = administrationAccordingToSchema;
        this.freeText = freeText;
        this.structures = structures;
        __WEBPACK_IMPORTED_MODULE_3__Validator__["a" /* Validator */].validate(this);
    }
    DosageWrapper.fromJsonObject = function (jsonObject) {
        return new DosageWrapper(__WEBPACK_IMPORTED_MODULE_0__AdministrationAccordingToSchemaWrapper__["a" /* AdministrationAccordingToSchemaWrapper */].fromJsonObject(jsonObject.administrationAccordingToSchema), __WEBPACK_IMPORTED_MODULE_1__FreeTextWrapper__["a" /* FreeTextWrapper */].fromJsonObject(jsonObject.freeText), __WEBPACK_IMPORTED_MODULE_2__StructuresWrapper__["a" /* StructuresWrapper */].fromJsonObject(jsonObject.structures));
    };
    DosageWrapper.makeStructuredDosage = function (structures) {
        return new DosageWrapper(undefined, undefined, structures);
    };
    DosageWrapper.makeFreeTextDosage = function (freeText) {
        return new DosageWrapper(undefined, freeText, undefined);
    };
    DosageWrapper.makeAccordingToSchemaDosage = function (administrationAccordingToSchema) {
        return new DosageWrapper(administrationAccordingToSchema, undefined, undefined);
    };
    /**
     * @return Returns true if the dosage is "according to schema..."
     */
    DosageWrapper.prototype.isAdministrationAccordingToSchema = function () {
        return this.administrationAccordingToSchema !== null && this.administrationAccordingToSchema !== undefined;
    };
    /**
     * @return Returns true if the dosage is a free text dosage
     */
    DosageWrapper.prototype.isFreeText = function () {
        return this.freeText !== undefined && this.freeText !== null;
    };
    /**
     * @return Returns true if the dosage is structured
     */
    DosageWrapper.prototype.isStructured = function () {
        return this.structures !== undefined && this.structures !== null;
    };
    return DosageWrapper;
}());



/***/ }),
/* 3 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LongTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__vowrapper_DayWrapper__ = __webpack_require__(12);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__DosisTilTekstException__ = __webpack_require__(5);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__TextHelper__ = __webpack_require__(0);



var LongTextConverterImpl = (function () {
    function LongTextConverterImpl() {
    }
    LongTextConverterImpl.prototype.getDosageStartText = function (startDateOrDateTime) {
        return "Doseringsforløbet starter " + this.datesToLongText(startDateOrDateTime);
    };
    LongTextConverterImpl.prototype.getDosageEndText = function (endDateOrDateTime) {
        return ", og ophører " + this.datesToLongText(endDateOrDateTime);
    };
    LongTextConverterImpl.prototype.datesToLongText = function (startDateOrDateTime) {
        if (!startDateOrDateTime)
            throw new __WEBPACK_IMPORTED_MODULE_1__DosisTilTekstException__["a" /* DosisTilTekstException */]("startDateOrDateTime must be set");
        if (startDateOrDateTime.getDate()) {
            return __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].formatLongDate(startDateOrDateTime.getDate());
        }
        else {
            var dateTime = startDateOrDateTime.getDateTime();
            // We do not want to show seconds precision if seconds are not specified or 0
            if (this.haveSeconds(dateTime)) {
                return __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].formatLongDateTime(dateTime);
            }
            else {
                return __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].formatLongDateNoSecs(dateTime);
            }
        }
    };
    LongTextConverterImpl.prototype.haveSeconds = function (dateTime) {
        return dateTime.getSeconds() !== 0;
    };
    LongTextConverterImpl.prototype.getDaysText = function (unitOrUnits, structure) {
        var s = "";
        var appendedLines = 0;
        for (var _i = 0, _a = structure.getDays(); _i < _a.length; _i++) {
            var day = _a[_i];
            appendedLines++;
            if (appendedLines > 1) {
                s += "\n";
            }
            s += __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].INDENT;
            var daysLabel = this.makeDaysLabel(structure, day);
            // We cannot have a day without label if there are other days (or "any day")
            if (structure.getDays().length > 1 && daysLabel.length === 0 && structure.getIterationInterval() === 1)
                daysLabel = "Daglig: ";
            else if (structure.getDays().length > 1 && daysLabel.length === 0 && structure.getIterationInterval() > 1)
                daysLabel = "Dosering: "; // We probably never get here
            s += daysLabel;
            s += this.makeDaysDosage(unitOrUnits, structure, day, daysLabel.length > 0);
        }
        return s;
    };
    LongTextConverterImpl.prototype.makeDaysLabel = function (structure, day) {
        if (day.getDayNumber() === 0) {
            if (day.containsAccordingToNeedDosesOnly())
                return "Efter behov: ";
            else
                return "Dag ikke angivet: ";
        }
        else if (structure.getIterationInterval() === 1) {
            return "";
        }
        else {
            return __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].makeDayString(structure.getStartDateOrDateTime(), day.getDayNumber()) + ": ";
        }
    };
    LongTextConverterImpl.prototype.makeDaysDosage = function (unitOrUnits, structure, day, hasDaysLabel) {
        var s = "";
        var supplText = "";
        if (structure.getSupplText()) {
            supplText = __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        }
        var daglig = "";
        if (!hasDaysLabel)
            daglig = " daglig";
        if (day.getNumberOfDoses() === 1) {
            s += this.makeOneDose(day.getDose(0), unitOrUnits, structure.getSupplText());
            if (day.containsAccordingToNeedDosesOnly() && day.getDayNumber() > 0)
                s += " højst 1 gang" + daglig + supplText;
            else if (!hasDaysLabel && day.containsPlainDose())
                s += " 1 gang" + daglig + supplText;
            else
                s += supplText;
        }
        else if (day.getNumberOfDoses() > 1 && day.allDosesAreTheSame()) {
            s += this.makeOneDose(day.getDose(0), unitOrUnits, structure.getSupplText());
            if (day.containsAccordingToNeedDosesOnly() && day.getDayNumber() > 0)
                s += " højst " + day.getNumberOfDoses() + " " + __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].gange(day.getNumberOfDoses()) + daglig + supplText;
            else
                s += " " + day.getNumberOfDoses() + " " + __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].gange(day.getNumberOfDoses()) + daglig + supplText;
        }
        else if (day.getNumberOfDoses() > 2 && day.allDosesButTheFirstAreTheSame()) {
            // Eks.: 1 stk. kl. 08:00 og 2 stk. 4 gange daglig
            s += this.makeOneDose(day.getDose(0), unitOrUnits, structure.getSupplText()) + supplText;
            if (0 < day.getNumberOfDoses() - 1) {
                s += " + ";
            }
            var dayWithoutFirstDose = new __WEBPACK_IMPORTED_MODULE_0__vowrapper_DayWrapper__["a" /* DayWrapper */](day.getDayNumber(), day.getAllDoses().slice(1, day.getAllDoses().length));
            s += this.makeDaysDosage(unitOrUnits, structure, dayWithoutFirstDose, false);
        }
        else {
            for (var d = 0; d < day.getNumberOfDoses(); d++) {
                s += this.makeOneDose(day.getDose(d), unitOrUnits, structure.getSupplText()) + supplText;
                if (d < day.getNumberOfDoses() - 1)
                    s += " + ";
            }
        }
        var dosagePeriodPostfix = structure.getDosagePeriodPostfix();
        if (dosagePeriodPostfix && dosagePeriodPostfix.length > 0) {
            s += " " + dosagePeriodPostfix;
        }
        return s;
    };
    LongTextConverterImpl.prototype.makeOneDose = function (dose, unitOrUnits, supplText) {
        var s = dose.getAnyDoseQuantityString();
        s += " " + __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].getUnit(dose, unitOrUnits);
        if (dose.getLabel().length > 0) {
            s += " " + dose.getLabel();
        }
        if (dose.getIsAccordingToNeed()) {
            s += " efter behov";
        }
        return s;
    };
    LongTextConverterImpl.prototype.getNoteText = function (structure) {
        if (this.isVarying(structure) && this.isComplex(structure))
            return ".\nBemærk at doseringen varierer og har et komplekst forløb:\n";
        else if (this.isVarying(structure))
            return ".\nBemærk at doseringen varierer:\n";
        else if (this.isComplex(structure))
            return ".\nBemærk at doseringen har et komplekst forløb:\n";
        else
            return ":\n";
    };
    LongTextConverterImpl.prototype.isComplex = function (structure) {
        if (structure.getDays().length === 1)
            return false;
        return !structure.daysAreInUninteruptedSequenceFromOne();
    };
    LongTextConverterImpl.prototype.isVarying = function (structure) {
        return !structure.allDaysAreTheSame();
    };
    return LongTextConverterImpl;
}());



/***/ }),
/* 4 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__TextHelper__ = __webpack_require__(0);

var DoseWrapper = (function () {
    function DoseWrapper(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) {
        this._doseQuantity = doseQuantity;
        this._minimalDoseQuantity = minimalDoseQuantity;
        this._maximalDoseQuantity = maximalDoseQuantity;
        this._isAccordingToNeed = isAccordingToNeed;
        if (minimalDoseQuantity !== undefined)
            this._minimalDoseQuantityString = __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].formatQuantity(minimalDoseQuantity);
        if (maximalDoseQuantity !== undefined)
            this._maximalDoseQuantityString = __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].formatQuantity(maximalDoseQuantity);
        if (doseQuantity !== undefined)
            this._doseQuantityString = __WEBPACK_IMPORTED_MODULE_0__TextHelper__["a" /* TextHelper */].formatQuantity(doseQuantity).replace(".", ",");
    }
    /*
    public static  to(value: number) {
        if(value==null)
            return null;
         v = new (value);
        v = v.setScale(9, .ROUND_HALF_UP);
        return v;
    }
      */
    DoseWrapper.isZero = function (quantity) {
        if (quantity) {
            return quantity < 0.000000001;
        }
        else {
            return true;
        }
    };
    DoseWrapper.isMinAndMaxZero = function (minimalQuantity, maximalQuantity) {
        return !minimalQuantity && !maximalQuantity;
    };
    DoseWrapper.prototype.getMinimalDoseQuantity = function () {
        return this._minimalDoseQuantity;
    };
    DoseWrapper.prototype.getMaximalDoseQuantity = function () {
        return this._maximalDoseQuantity;
    };
    DoseWrapper.prototype.getDoseQuantity = function () {
        return this._doseQuantity;
    };
    DoseWrapper.prototype.getMinimalDoseQuantityString = function () {
        return this._minimalDoseQuantityString;
    };
    DoseWrapper.prototype.getMaximalDoseQuantityString = function () {
        return this._maximalDoseQuantityString;
    };
    DoseWrapper.prototype.getDoseQuantityString = function () {
        return this._doseQuantityString;
    };
    DoseWrapper.prototype.getIsAccordingToNeed = function () {
        return this._isAccordingToNeed;
    };
    DoseWrapper.prototype.getAnyDoseQuantityString = function () {
        if (this.getDoseQuantityString())
            return this.getDoseQuantityString();
        else
            return this.getMinimalDoseQuantityString() + "-" + this.getMaximalDoseQuantityString();
    };
    DoseWrapper.prototype.theSameAs = function (other) {
        if (this.getLabel() !== other.getLabel())
            return false;
        if (this.getIsAccordingToNeed() !== other.getIsAccordingToNeed())
            return false;
        if (!this.equalsWhereNullsAreTrue(this.getMinimalDoseQuantityString(), other.getMinimalDoseQuantityString()))
            return false;
        if (!this.equalsWhereNullsAreTrue(this.getMaximalDoseQuantityString(), other.getMaximalDoseQuantityString()))
            return false;
        if (!this.equalsWhereNullsAreTrue(this.getDoseQuantityString(), other.getDoseQuantityString()))
            return false;
        return true;
    };
    DoseWrapper.prototype.equalsWhereNullsAreTrue = function (a, b) {
        if (a === undefined && b === undefined)
            return true;
        else if ((a === undefined && b) || (a && b === undefined)) {
            return false;
        }
        else {
            return a === b;
        }
    };
    return DoseWrapper;
}());



/***/ }),
/* 5 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosisTilTekstException; });
var DosisTilTekstException = (function () {
    function DosisTilTekstException(msg) {
        this.message = msg;
    }
    DosisTilTekstException.prototype.getMessage = function () {
        return this.message;
    };
    return DosisTilTekstException;
}());



/***/ }),
/* 6 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DateOrDateTimeWrapper; });
var DateOrDateTimeWrapper = (function () {
    function DateOrDateTimeWrapper(date, dateTime) {
        this.date = date;
        this.dateTime = dateTime;
    }
    DateOrDateTimeWrapper.fromJsonObject = function (jsonObject) {
        if (jsonObject) {
            return new DateOrDateTimeWrapper(jsonObject.date ? new Date(jsonObject.date) : undefined, jsonObject.dateTime ? new Date(jsonObject.dateTime) : undefined);
        }
        return undefined;
    };
    DateOrDateTimeWrapper.prototype.getDate = function () {
        return this.date;
    };
    DateOrDateTimeWrapper.prototype.getDateTime = function () {
        return this.dateTime;
    };
    DateOrDateTimeWrapper.prototype.getDateOrDateTime = function () {
        if (this.date)
            return this.date;
        else
            return this.dateTime;
    };
    DateOrDateTimeWrapper.prototype.isEqualTo = function (dt) {
        if (dt) {
            if (this.getDate() && dt.getDate())
                return this.getDate().getTime() === dt.getDate().getTime();
            else if (this.getDateTime() && dt.getDateTime())
                return this.getDateTime().getTime() === dt.getDateTime().getTime();
            else
                return !dt.getDate() && !dt.getDateTime();
        }
        return false;
    };
    return DateOrDateTimeWrapper;
}());



/***/ }),
/* 7 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StructuresWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__UnitOrUnitsWrapper__ = __webpack_require__(25);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__StructureWrapper__ = __webpack_require__(13);


var StructuresWrapper = (function () {
    function StructuresWrapper(unitOrUnits, structures) {
        this.unitOrUnits = unitOrUnits;
        structures.sort(function (s1, s2) {
            var i = s1.getStartDateOrDateTime().getDateOrDateTime().getTime() - s2.getStartDateOrDateTime().getDateOrDateTime().getTime();
            if (i !== 0)
                return i;
            if (s1.containsAccordingToNeedDosesOnly())
                return 1;
            else
                return -1;
        });
        this.structures = structures;
    }
    StructuresWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new StructuresWrapper(__WEBPACK_IMPORTED_MODULE_0__UnitOrUnitsWrapper__["a" /* UnitOrUnitsWrapper */].fromJsonObject(jsonObject.unitOrUnits), jsonObject.structures.map(function (s) { return __WEBPACK_IMPORTED_MODULE_1__StructureWrapper__["a" /* StructureWrapper */].fromJsonObject(s); }))
            : undefined;
    };
    StructuresWrapper.prototype.getUnitOrUnits = function () {
        return this.unitOrUnits;
    };
    StructuresWrapper.prototype.getStructures = function () {
        return this.structures;
    };
    StructuresWrapper.prototype.hasOverlappingPeriodes = function () {
        for (var i = 0; i < this.getStructures().length; i++) {
            for (var j = i + 1; j < this.getStructures().length; j++) {
                var dis = this.getStructures()[i].getStartDateOrDateTime();
                var die = this.getStructures()[i].getEndDateOrDateTime();
                var djs = this.getStructures()[j].getStartDateOrDateTime();
                var dje = this.getStructures()[j].getEndDateOrDateTime();
                if (this.overlaps(dis, die, djs, dje))
                    return true;
            }
        }
        return false;
    };
    StructuresWrapper.prototype.overlaps = function (dis, die, djs, dje) {
        var cis = this.makeStart(dis);
        var cjs = this.makeStart(djs);
        if (cis.getTime() === cjs.getTime()) {
            return true;
        }
        var cie = this.makeEnd(die);
        var cje = this.makeEnd(dje);
        if (cis.getTime() < cjs.getTime()) {
            return cie.getTime() >= cje.getTime();
        }
        else {
            return cje.getTime() >= cie.getTime();
        }
    };
    StructuresWrapper.prototype.makeStart = function (ds) {
        var d;
        if (ds && ds.getDateTime()) {
            d = new Date(ds.getDateTime().getTime());
            d.setMilliseconds(0);
        }
        else if (ds && ds.getDate()) {
            d = new Date(ds.getDate().getTime());
            d.setHours(0);
            d.setMinutes(0);
            d.setSeconds(0);
            d.setMilliseconds(0);
        }
        else {
            d = new Date(2000, 0, 1, 0, 0, 0);
        }
        return d;
    };
    StructuresWrapper.prototype.makeEnd = function (de) {
        var d;
        if (de && de.getDateTime()) {
            d = new Date(de.getDateTime().getTime());
            d.setMilliseconds(0);
        }
        else if (de && de.getDate()) {
            d = new Date(de.getDate().getTime());
            d.setHours(0);
            d.setMinutes(0);
            d.setSeconds(0);
            d.setMilliseconds(0);
        }
        else {
            d = new Date(2999, 11, 31, 23, 59, 59, 0);
        }
        return d;
    };
    return StructuresWrapper;
}());



/***/ }),
/* 8 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ShortTextConverter; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__shorttextconverterimpl_AdministrationAccordingToSchemaConverterImpl__ = __webpack_require__(40);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__shorttextconverterimpl_FreeTextConverterImpl__ = __webpack_require__(41);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shorttextconverterimpl_MorningNoonEveningNightEyeOrEarConverterImpl__ = __webpack_require__(43);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shorttextconverterimpl_MorningNoonEveningNightConverterImpl__ = __webpack_require__(9);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shorttextconverterimpl_SimpleLimitedAccordingToNeedConverterImpl__ = __webpack_require__(18);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__shorttextconverterimpl_MorningNoonEveningNightAndAccordingToNeedConverterImpl__ = __webpack_require__(42);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__shorttextconverterimpl_WeeklyMorningNoonEveningNightConverterImpl__ = __webpack_require__(44);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__shorttextconverterimpl_RepeatedEyeOrEarConverterImpl__ = __webpack_require__(68);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__shorttextconverterimpl_RepeatedConverterImpl__ = __webpack_require__(67);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__shorttextconverterimpl_SimpleNonRepeatedConverterImpl__ = __webpack_require__(70);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__shorttextconverterimpl_MorningNoonEveningNightInNDaysConverterImpl__ = __webpack_require__(63);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__shorttextconverterimpl_SimpleAccordingToNeedConverterImpl__ = __webpack_require__(69);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__shorttextconverterimpl_LimitedNumberOfDaysConverterImpl__ = __webpack_require__(62);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__shorttextconverterimpl_WeeklyRepeatedConverterImpl__ = __webpack_require__(45);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__shorttextconverterimpl_ParacetamolConverterImpl__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_15__shorttextconverterimpl_MultipleDaysNonRepeatedConverterImpl__ = __webpack_require__(64);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_16__shorttextconverterimpl_NumberOfWholeWeeksConverterImpl__ = __webpack_require__(65);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_17__shorttextconverterimpl_DayInWeekConverterImpl__ = __webpack_require__(61);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_18__shorttextconverterimpl_CombinedTwoPeriodesConverterImpl__ = __webpack_require__(60);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_19__vowrapper_DosageWrapper__ = __webpack_require__(2);




















var ShortTextConverter = (function () {
    /**
     * Populate a list of implemented converters
     * Consider the order: The tests are evaluated in order, adding the most likely to succeed
     * first improves performance
     */
    function ShortTextConverter() {
        ShortTextConverter._converters = [
            new __WEBPACK_IMPORTED_MODULE_0__shorttextconverterimpl_AdministrationAccordingToSchemaConverterImpl__["a" /* AdministrationAccordingToSchemaConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_1__shorttextconverterimpl_FreeTextConverterImpl__["a" /* FreeTextConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_2__shorttextconverterimpl_MorningNoonEveningNightEyeOrEarConverterImpl__["a" /* MorningNoonEveningNightEyeOrEarConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_3__shorttextconverterimpl_MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_6__shorttextconverterimpl_WeeklyMorningNoonEveningNightConverterImpl__["a" /* WeeklyMorningNoonEveningNightConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_7__shorttextconverterimpl_RepeatedEyeOrEarConverterImpl__["a" /* RepeatedEyeOrEarConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_8__shorttextconverterimpl_RepeatedConverterImpl__["a" /* RepeatedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_9__shorttextconverterimpl_SimpleNonRepeatedConverterImpl__["a" /* SimpleNonRepeatedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_10__shorttextconverterimpl_MorningNoonEveningNightInNDaysConverterImpl__["a" /* MorningNoonEveningNightInNDaysConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_11__shorttextconverterimpl_SimpleAccordingToNeedConverterImpl__["a" /* SimpleAccordingToNeedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_12__shorttextconverterimpl_LimitedNumberOfDaysConverterImpl__["a" /* LimitedNumberOfDaysConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_4__shorttextconverterimpl_SimpleLimitedAccordingToNeedConverterImpl__["a" /* SimpleLimitedAccordingToNeedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_13__shorttextconverterimpl_WeeklyRepeatedConverterImpl__["a" /* WeeklyRepeatedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_14__shorttextconverterimpl_ParacetamolConverterImpl__["a" /* ParacetamolConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_5__shorttextconverterimpl_MorningNoonEveningNightAndAccordingToNeedConverterImpl__["a" /* MorningNoonEveningNightAndAccordingToNeedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_15__shorttextconverterimpl_MultipleDaysNonRepeatedConverterImpl__["a" /* MultipleDaysNonRepeatedConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_16__shorttextconverterimpl_NumberOfWholeWeeksConverterImpl__["a" /* NumberOfWholeWeeksConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_17__shorttextconverterimpl_DayInWeekConverterImpl__["a" /* DayInWeekConverterImpl */](),
            new __WEBPACK_IMPORTED_MODULE_18__shorttextconverterimpl_CombinedTwoPeriodesConverterImpl__["a" /* CombinedTwoPeriodesConverterImpl */]()
        ];
    }
    ShortTextConverter.getInstance = function () { return ShortTextConverter._instance; };
    ShortTextConverter.prototype.getConverterClassName = function (dosageJson) {
        return this.getConverterClassNameWrapper(__WEBPACK_IMPORTED_MODULE_19__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson));
    };
    ShortTextConverter.prototype.getConverterClassNameStr = function (jsonStr) {
        if (jsonStr === undefined || jsonStr === null) {
            return null;
        }
        return this.getConverterClassName(JSON.parse(jsonStr));
    };
    ShortTextConverter.prototype.getConverterClassNameWrapper = function (dosage) {
        for (var _i = 0, _a = ShortTextConverter._converters; _i < _a.length; _i++) {
            var converter = _a[_i];
            if (converter.canConvert(dosage) && converter.doConvert(dosage).length <= ShortTextConverter.MAX_LENGTH) {
                return converter.constructor["name"];
            }
        }
        return null;
    };
    /**
     * Performs a conversion to a short text if possible. Otherwise null.
     * @param dosage
     * @return A short text string describing the dosage
     */
    ShortTextConverter.prototype.convertWrapper = function (dosage, maxLength) {
        if (maxLength === void 0) { maxLength = ShortTextConverter.MAX_LENGTH; }
        return ShortTextConverter.getInstance().doConvert(dosage, maxLength);
    };
    ShortTextConverter.prototype.convert = function (dosageJson, maxLength) {
        if (maxLength === void 0) { maxLength = ShortTextConverter.MAX_LENGTH; }
        if (dosageJson === undefined || dosageJson === null) {
            return null;
        }
        var dosage = __WEBPACK_IMPORTED_MODULE_19__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson);
        return ShortTextConverter.getInstance().doConvert(dosage, maxLength);
    };
    ShortTextConverter.prototype.convertStr = function (jsonStr) {
        if (jsonStr === undefined || jsonStr === null) {
            return null;
        }
        return this.convert(JSON.parse(jsonStr));
    };
    /**
     * Performs a conversion to a short text with a custom maximum length. Returns translation if possible, otherwise null.
     * @param dosage
     * @param maxLength
     * @return A short text string describing the dosage
     */
    ShortTextConverter.prototype.doConvert = function (dosage, maxLength) {
        for (var _i = 0, _a = ShortTextConverter._converters; _i < _a.length; _i++) {
            var converter = _a[_i];
            if (converter.canConvert(dosage)) {
                var s = converter.doConvert(dosage);
                if (s.length <= maxLength)
                    return s;
            }
        }
        return null;
    };
    ShortTextConverter.prototype.canConvert = function (dosage) {
        for (var _i = 0, _a = ShortTextConverter._converters; _i < _a.length; _i++) {
            var converter = _a[_i];
            if (converter.canConvert(dosage)) {
                return true;
            }
        }
        return false;
    };
    ShortTextConverter.MAX_LENGTH = 70;
    ShortTextConverter._instance = new ShortTextConverter();
    return ShortTextConverter;
}());



/***/ }),
/* 9 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MorningNoonEveningNightConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var MorningNoonEveningNightConverterImpl = (function (_super) {
    __extends(MorningNoonEveningNightConverterImpl, _super);
    function MorningNoonEveningNightConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MorningNoonEveningNightConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.getDayNumber() !== 1)
            return false;
        if (day.containsPlainDose() || day.containsTimedDose())
            return false;
        return true;
    };
    MorningNoonEveningNightConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += MorningNoonEveningNightConverterImpl.getMorningText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightConverterImpl.getNoonText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightConverterImpl.getEveningText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightConverterImpl.getNightText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightConverterImpl.getSupplText(structure.getSupplText());
        return text;
    };
    MorningNoonEveningNightConverterImpl.getMorningText = function (day, unitOrUnits) {
        var text = "";
        if (day.getMorningDose()) {
            text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getMorningDose(), unitOrUnits);
            if (day.getMorningDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightConverterImpl.getNoonText = function (day, unitOrUnits) {
        var text = "";
        if (day.getNoonDose()) {
            // Er der en morgen-dosis og kommer der en efterflg. dosistekst ELLER der er tale om blandet PN/fast, brug ","
            if (day.getMorningDose() && ((day.getEveningDose() || day.getNightDose()) || !day.containsOnlyPNOrFixedDoses()))
                text += ", ";
            else if (day.getMorningDose() && day.containsOnlyPNOrFixedDoses())
                text += " og ";
            if (!day.allDosesHaveTheSameQuantity() || !day.containsOnlyPNOrFixedDoses())
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNoonDose(), unitOrUnits);
            else if (day.getMorningDose())
                text += day.getNoonDose().getLabel();
            else
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNoonDose(), unitOrUnits);
            if (day.getNoonDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightConverterImpl.getEveningText = function (day, unitOrUnits) {
        var text = "";
        if (day.getEveningDose()) {
            // Er der en morgen/midag-dosis og kommer der en efterflg. dosistekst ELLER der er tale om blandet PN/fast, brug ","
            if ((day.getMorningDose() || day.getNoonDose()) && ((day.getNightDose() || !day.containsOnlyPNOrFixedDoses())))
                text += ", ";
            else if ((day.getMorningDose() || day.getNoonDose()) && day.containsOnlyPNOrFixedDoses())
                text += " og ";
            if (!day.allDosesHaveTheSameQuantity() || !day.containsOnlyPNOrFixedDoses())
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getEveningDose(), unitOrUnits);
            else if (day.getMorningDose() || day.getNoonDose())
                text += day.getEveningDose().getLabel();
            else
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getEveningDose(), unitOrUnits);
            if (day.getEveningDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightConverterImpl.getNightText = function (day, unitOrUnits) {
        var text = "";
        if (day.getNightDose()) {
            if (day.getMorningDose() || day.getNoonDose() || day.getEveningDose()) {
                if (day.containsOnlyPNOrFixedDoses()) {
                    text += " og ";
                }
                else {
                    text += ", ";
                }
            }
            if (!day.allDosesHaveTheSameQuantity() || !day.containsOnlyPNOrFixedDoses())
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNightDose(), unitOrUnits);
            else if (day.getMorningDose() || day.getNoonDose() || day.getEveningDose())
                text += day.getNightDose().getLabel();
            else
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNightDose(), unitOrUnits);
            if (day.getNightDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightConverterImpl.getSupplText = function (supplText) {
        if (supplText) {
            return __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(supplText) + supplText;
        }
        return "";
    };
    return MorningNoonEveningNightConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 10 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LongTextConverter; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__longtextconverterimpl_DailyRepeatedConverterImpl__ = __webpack_require__(34);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__longtextconverterimpl_FreeTextConverterImpl__ = __webpack_require__(38);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__longtextconverterimpl_AdministrationAccordingToSchemaConverterImpl__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__longtextconverterimpl_DefaultLongTextConverterImpl__ = __webpack_require__(35);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__longtextconverterimpl_EmptyStructureConverterImpl__ = __webpack_require__(37);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__longtextconverterimpl_TwoDaysRepeatedConverterImpl__ = __webpack_require__(39);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__longtextconverterimpl_WeeklyRepeatedConverterImpl__ = __webpack_require__(11);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__longtextconverterimpl_DefaultMultiPeriodeLongTextConverterImpl__ = __webpack_require__(36);









var LongTextConverter = (function () {
    function LongTextConverter() {
        LongTextConverter._converters = new Array();
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_3__longtextconverterimpl_AdministrationAccordingToSchemaConverterImpl__["a" /* AdministrationAccordingToSchemaConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_2__longtextconverterimpl_FreeTextConverterImpl__["a" /* FreeTextConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_5__longtextconverterimpl_EmptyStructureConverterImpl__["a" /* EmptyStructureConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_1__longtextconverterimpl_DailyRepeatedConverterImpl__["a" /* DailyRepeatedConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_6__longtextconverterimpl_TwoDaysRepeatedConverterImpl__["a" /* TwoDaysRepeatedConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_7__longtextconverterimpl_WeeklyRepeatedConverterImpl__["a" /* WeeklyRepeatedConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_4__longtextconverterimpl_DefaultLongTextConverterImpl__["a" /* DefaultLongTextConverterImpl */]());
        LongTextConverter._converters.push(new __WEBPACK_IMPORTED_MODULE_8__longtextconverterimpl_DefaultMultiPeriodeLongTextConverterImpl__["a" /* DefaultMultiPeriodeLongTextConverterImpl */](this));
    }
    LongTextConverter.getInstance = function () { return LongTextConverter._instance; };
    LongTextConverter.prototype.convertStr = function (jsonStr) {
        if (jsonStr === undefined || jsonStr === null) {
            return null;
        }
        return this.convert(JSON.parse(jsonStr));
    };
    LongTextConverter.prototype.convert = function (dosageJson) {
        if (dosageJson === undefined || dosageJson === null) {
            return null;
        }
        var dosage = __WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson);
        return this.convertWrapper(dosage);
    };
    LongTextConverter.prototype.convertWrapper = function (dosage) {
        for (var _i = 0, _a = LongTextConverter._converters; _i < _a.length; _i++) {
            var converter = _a[_i];
            if (converter.canConvert(dosage)) {
                return converter.doConvert(dosage);
            }
        }
        return null;
    };
    /*
    public static getConverter(dosage: DosageWrapper): LongTextConverterImpl {
        for (let converter of this.getConverters()) {
            if (converter.canConvert(dosage))
                return converter;
        }
        return null;
    } */
    LongTextConverter.prototype.getConverterClassName = function (dosageJson) {
        return this.getConverterClassNameWrapper(__WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson));
    };
    LongTextConverter.prototype.getConverterClassNameStr = function (jsonStr) {
        if (jsonStr === undefined || jsonStr === null) {
            return null;
        }
        return this.getConverterClassName(JSON.parse(jsonStr));
    };
    LongTextConverter.prototype.getConverterClassNameWrapper = function (dosage) {
        for (var _i = 0, _a = LongTextConverter._converters; _i < _a.length; _i++) {
            var converter = _a[_i];
            if (converter.canConvert(dosage)) {
                return converter.constructor["name"];
            }
        }
        return null;
    };
    LongTextConverter._instance = new LongTextConverter();
    return LongTextConverter;
}());



/***/ }),
/* 11 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return WeeklyRepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var WeeklyRepeatedConverterImpl = (function (_super) {
    __extends(WeeklyRepeatedConverterImpl, _super);
    function WeeklyRepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    WeeklyRepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures) {
            if (dosage.structures.getStructures().length !== 1)
                return false;
            var structure = dosage.structures.getStructures()[0];
            if (structure.getIterationInterval() !== 7)
                return false;
            if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
                return false;
            if (structure.getDays().length > 7)
                return false;
            if (structure.getDays()[0].getDayNumber() === 0)
                return false;
            if (structure.getDays()[structure.getDays().length - 1].getDayNumber() > 7)
                return false;
            return true;
        }
        return false;
    };
    WeeklyRepeatedConverterImpl.prototype.doConvert = function (dosage) {
        return this.convert(dosage.structures.getUnitOrUnits(), dosage.structures.getStructures()[0]);
    };
    WeeklyRepeatedConverterImpl.prototype.convert = function (unitOrUnits, structure) {
        var s = "";
        s += this.getDosageStartText(structure.getStartDateOrDateTime());
        s += ", forløbet gentages hver uge";
        if (structure.getEndDateOrDateTime()) {
            s += this.getDosageEndText(structure.getEndDateOrDateTime());
        }
        s += this.getNoteText(structure);
        s += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].INDENT + "Doseringsforløb:\n";
        s += this.getDayNamesText(unitOrUnits, structure);
        return s;
    };
    WeeklyRepeatedConverterImpl.prototype.getDayNamesText = function (unitOrUnits, structure) {
        // Make a sorted list of weekdays
        var s = "";
        var daysOfWeek = WeeklyRepeatedConverterImpl.sortDaysOfWeek(structure);
        var appendedLines = 0;
        for (var _i = 0, daysOfWeek_1 = daysOfWeek; _i < daysOfWeek_1.length; _i++) {
            var e = daysOfWeek_1[_i];
            if (appendedLines > 0)
                s += "\n";
            appendedLines++;
            s += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].INDENT + e.getName() + ": ";
            s += this.makeDaysDosage(unitOrUnits, structure, e.getDay(), true);
        }
        return s;
    };
    WeeklyRepeatedConverterImpl.sortDaysOfWeek = function (structure) {
        // Convert all days (up to 7) to day of week and DK name ((1, Mandag) etc).
        // Sort according to day of week (Monday always first) using DayOfWeek's compareTo in SortedSet
        var daysOfWeekSet = structure.getDays().map(function (day) { return __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].makeDayOfWeekAndName(structure.getStartDateOrDateTime(), day, true); });
        return daysOfWeekSet.sort(WeeklyRepeatedConverterImpl.daySort);
    };
    // Javascript day 0 = Sunday meaning special sorting of days
    WeeklyRepeatedConverterImpl.daySort = function (day1, day2) {
        var sortDay1 = day1.getDayOfWeek() === 0 ? 8 : day1.getDayOfWeek();
        var sortDay2 = day2.getDayOfWeek() === 0 ? 8 : day2.getDayOfWeek();
        return sortDay1 - sortDay2;
    };
    return WeeklyRepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 12 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DayWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__ = __webpack_require__(5);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__PlainDoseWrapper__ = __webpack_require__(24);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__TimedDoseWrapper__ = __webpack_require__(48);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__MorningDoseWrapper__ = __webpack_require__(21);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__NoonDoseWrapper__ = __webpack_require__(23);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__EveningDoseWrapper__ = __webpack_require__(19);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__NightDoseWrapper__ = __webpack_require__(22);







var DayWrapper = (function () {
    function DayWrapper(dayNumber, doses) {
        // Doses were separate types before 2012-06-01. We keep them for now to maintain
        // compatibility in the dosis-to-text conversion
        // AccordingToNeed is merged into each type since 2012-06-01 schemas
        // private List<AccordingToNeedDoseWrapper> accordingToNeedDoses = new ArrayList<AccordingToNeedDoseWrapper>();
        this.plainDoses = [];
        this.timedDoses = [];
        this.allDoses = doses;
        for (var _i = 0, doses_1 = doses; _i < doses_1.length; _i++) {
            var dose = doses_1[_i];
            this.dayNumber = dayNumber;
            if (dose) {
                if (dose instanceof __WEBPACK_IMPORTED_MODULE_1__PlainDoseWrapper__["a" /* PlainDoseWrapper */])
                    this.plainDoses.push(dose);
                else if (dose instanceof __WEBPACK_IMPORTED_MODULE_2__TimedDoseWrapper__["a" /* TimedDoseWrapper */])
                    this.timedDoses.push(dose);
                else if (dose instanceof __WEBPACK_IMPORTED_MODULE_3__MorningDoseWrapper__["a" /* MorningDoseWrapper */])
                    this.morningDose = dose;
                else if (dose instanceof __WEBPACK_IMPORTED_MODULE_4__NoonDoseWrapper__["a" /* NoonDoseWrapper */])
                    this.noonDose = dose;
                else if (dose instanceof __WEBPACK_IMPORTED_MODULE_5__EveningDoseWrapper__["a" /* EveningDoseWrapper */])
                    this.eveningDose = dose;
                else if (dose instanceof __WEBPACK_IMPORTED_MODULE_6__NightDoseWrapper__["a" /* NightDoseWrapper */])
                    this.nightDose = dose;
            }
            this.areAllDosesTheSame = true;
            var compareDose = void 0;
            for (var _a = 0, _b = this.getAllDoses(); _a < _b.length; _a++) {
                var dose_1 = _b[_a];
                if (!compareDose) {
                    compareDose = dose_1;
                }
                else if (!compareDose.theSameAs(dose_1)) {
                    this.areAllDosesTheSame = false;
                    break;
                }
            }
        }
    }
    DayWrapper.fromJsonObject = function (jsonObject) {
        if (jsonObject) {
            var allDoses_1 = [];
            jsonObject.allDoses.forEach(function (d) {
                switch (d.type) {
                    case "MorningDoseWrapper":
                        allDoses_1.push(__WEBPACK_IMPORTED_MODULE_3__MorningDoseWrapper__["a" /* MorningDoseWrapper */].fromJsonObject(d));
                        break;
                    case "NoonDoseWrapper":
                        allDoses_1.push(__WEBPACK_IMPORTED_MODULE_4__NoonDoseWrapper__["a" /* NoonDoseWrapper */].fromJsonObject(d));
                        break;
                    case "EveningDoseWrapper":
                        allDoses_1.push(__WEBPACK_IMPORTED_MODULE_5__EveningDoseWrapper__["a" /* EveningDoseWrapper */].fromJsonObject(d));
                        break;
                    case "NightDoseWrapper":
                        allDoses_1.push(__WEBPACK_IMPORTED_MODULE_6__NightDoseWrapper__["a" /* NightDoseWrapper */].fromJsonObject(d));
                        break;
                    case "PlainDoseWrapper":
                        allDoses_1.push(__WEBPACK_IMPORTED_MODULE_1__PlainDoseWrapper__["a" /* PlainDoseWrapper */].fromJsonObject(d));
                        break;
                    case "TimedDoseWrapper":
                        allDoses_1.push(__WEBPACK_IMPORTED_MODULE_2__TimedDoseWrapper__["a" /* TimedDoseWrapper */].fromJsonObject(d));
                        break;
                }
            });
            return new DayWrapper(jsonObject.dayNumber, allDoses_1);
        }
        return undefined;
    };
    DayWrapper.prototype.getDayNumber = function () {
        return this.dayNumber;
    };
    DayWrapper.prototype.getNumberOfDoses = function () {
        return this.allDoses.length;
    };
    DayWrapper.prototype.getDose = function (index) {
        return this.allDoses[index];
    };
    DayWrapper.prototype.getNumberOfAccordingToNeedDoses = function () {
        return this.getAccordingToNeedDoses().length;
    };
    DayWrapper.prototype.getAccordingToNeedDoses = function () {
        // Since the 2012/06/01 namespace "according to need" is just a flag
        if (this.accordingToNeedDoses) {
            return this.accordingToNeedDoses;
        }
        else {
            this.accordingToNeedDoses = new Array();
            for (var _i = 0, _a = this.allDoses; _i < _a.length; _i++) {
                var d = _a[_i];
                if (d.getIsAccordingToNeed())
                    this.accordingToNeedDoses.push(d);
            }
            return this.accordingToNeedDoses;
        }
    };
    DayWrapper.prototype.getPlainDoses = function () {
        return this.plainDoses;
    };
    DayWrapper.prototype.getNumberOfPlainDoses = function () {
        return this.plainDoses.length;
    };
    DayWrapper.prototype.getMorningDose = function () {
        return this.morningDose;
    };
    DayWrapper.prototype.getNoonDose = function () {
        return this.noonDose;
    };
    DayWrapper.prototype.getEveningDose = function () {
        return this.eveningDose;
    };
    DayWrapper.prototype.getNightDose = function () {
        return this.nightDose;
    };
    DayWrapper.prototype.getAllDoses = function () {
        return this.allDoses;
    };
    /**
     * Compares dosage quantities and the dosages label (the type of the dosage)
     * @return true if all dosages are of the same type and has the same quantity
     */
    DayWrapper.prototype.allDosesAreTheSame = function () {
        return this.areAllDosesTheSame;
    };
    /**
     * Compares dosage quantities (but not the dosages label)
     * @return true if all dosages has the same quantity
     */
    DayWrapper.prototype.allDosesHaveTheSameQuantity = function () {
        if (this.areAllDosesHaveTheSameQuantity) {
            return this.areAllDosesHaveTheSameQuantity;
        }
        this.areAllDosesHaveTheSameQuantity = true;
        if (this.getAllDoses().length > 1) {
            var dose0 = this.getAllDoses()[0];
            for (var i = 1; i < this.getAllDoses().length; i++) {
                if (dose0.getAnyDoseQuantityString() !== this.getAllDoses()[i].getAnyDoseQuantityString()) {
                    this.areAllDosesHaveTheSameQuantity = false;
                    break;
                }
            }
        }
        return this.areAllDosesHaveTheSameQuantity;
    };
    DayWrapper.prototype.allDosesButTheFirstAreTheSame = function () {
        if (this.areAllDosesExceptTheFirstTheSame) {
            return this.areAllDosesExceptTheFirstTheSame;
        }
        else {
            this.areAllDosesExceptTheFirstTheSame = true;
            var dose0 = void 0;
            for (var i = 1; i < this.getNumberOfDoses(); i++) {
                if (!dose0) {
                    dose0 = this.getAllDoses()[i];
                }
                else if (!dose0.theSameAs(this.getAllDoses()[i])) {
                    this.areAllDosesExceptTheFirstTheSame = false;
                    break;
                }
            }
        }
        return this.areAllDosesExceptTheFirstTheSame;
    };
    DayWrapper.prototype.containsOnlyPNOrFixedDoses = function () {
        return this.containsAccordingToNeedDosesOnly() || this.containsFixedDosesOnly();
    };
    DayWrapper.prototype.containsAccordingToNeedDose = function () {
        return this.getAllDoses().some(function (dose) { return dose.getIsAccordingToNeed(); });
    };
    DayWrapper.prototype.containsTimedDose = function () {
        return this.getAllDoses().some(function (dose) { return dose instanceof __WEBPACK_IMPORTED_MODULE_2__TimedDoseWrapper__["a" /* TimedDoseWrapper */]; });
    };
    DayWrapper.prototype.containsPlainDose = function () {
        return this.getAllDoses().some(function (dose) { return dose instanceof __WEBPACK_IMPORTED_MODULE_1__PlainDoseWrapper__["a" /* PlainDoseWrapper */]; });
    };
    DayWrapper.prototype.containsPlainNotAccordingToNeedDose = function () {
        return this.getAllDoses().some(function (dose) { return dose instanceof __WEBPACK_IMPORTED_MODULE_1__PlainDoseWrapper__["a" /* PlainDoseWrapper */] && !dose.getIsAccordingToNeed(); });
    };
    DayWrapper.prototype.containsMorningNoonEveningNightDoses = function () {
        return this.getAllDoses().some(function (dose) { return dose instanceof __WEBPACK_IMPORTED_MODULE_3__MorningDoseWrapper__["a" /* MorningDoseWrapper */] || dose instanceof __WEBPACK_IMPORTED_MODULE_4__NoonDoseWrapper__["a" /* NoonDoseWrapper */]
            || dose instanceof __WEBPACK_IMPORTED_MODULE_5__EveningDoseWrapper__["a" /* EveningDoseWrapper */] || dose instanceof __WEBPACK_IMPORTED_MODULE_6__NightDoseWrapper__["a" /* NightDoseWrapper */]; });
    };
    DayWrapper.prototype.containsAccordingToNeedDosesOnly = function () {
        return this.getAllDoses().every(function (d) { return d.getIsAccordingToNeed(); });
    };
    DayWrapper.prototype.containsFixedDosesOnly = function () {
        return this.getAllDoses().every(function (d) { return !d.getIsAccordingToNeed(); });
    };
    DayWrapper.prototype.getSumOfDoses = function () {
        var minValue = DayWrapper.newDosage();
        var maxValue = DayWrapper.newDosage();
        for (var _i = 0, _a = this.getAllDoses(); _i < _a.length; _i++) {
            var dose = _a[_i];
            if (dose.getDoseQuantity() !== undefined) {
                minValue = DayWrapper.addDosage(minValue, dose.getDoseQuantity());
                maxValue = DayWrapper.addDosage(maxValue, dose.getDoseQuantity());
            }
            else if (dose.getMinimalDoseQuantity() !== undefined && dose.getMaximalDoseQuantity() !== undefined) {
                minValue = DayWrapper.addDosage(minValue, dose.getMinimalDoseQuantity());
                maxValue = DayWrapper.addDosage(maxValue, dose.getMaximalDoseQuantity());
            }
            else {
                throw new __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__["a" /* DosisTilTekstException */]("DoseQuantity eller minimalDoseQuantity+MaximalDoseQuantity skal være sat");
            }
        }
        return { minimum: minValue, maximum: maxValue };
    };
    DayWrapper.newDosage = function () {
        return 0;
        /* TODO Kan dette udelades?
        BigDecimal v = new BigDecimal(0.0);
        v = v.setScale(9, BigDecimal.ROUND_HALF_UP);
        return v; */
    };
    DayWrapper.addDosage = function (bd, d) {
        if (d !== undefined) {
            return bd + d;
        }
        throw new __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__["a" /* DosisTilTekstException */]("addDosage: d skal være sat");
    };
    return DayWrapper;
}());



/***/ }),
/* 13 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StructureWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__DayWrapper__ = __webpack_require__(12);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__DosisTilTekstException__ = __webpack_require__(5);



var StructureWrapper = (function () {
    function StructureWrapper(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, days, dosagePeriodPostfix) {
        this.iterationInterval = iterationInterval;
        this.supplText = supplText;
        this.startDateOrDateTime = startDateOrDateTime;
        this.endDateOrDateTime = endDateOrDateTime;
        this.dosagePeriodPostfix = dosagePeriodPostfix;
        if (days) {
            this.days = days.sort(function (d1, d2) { return d1.getDayNumber() - d2.getDayNumber(); });
        }
        else {
            throw new __WEBPACK_IMPORTED_MODULE_2__DosisTilTekstException__["a" /* DosisTilTekstException */]("StructureWrapper: days must be set in StructureWrapper");
        }
    }
    StructureWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new StructureWrapper(jsonObject.iterationInterval, jsonObject.supplText, __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */].fromJsonObject(jsonObject.startDateOrDateTime), __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */].fromJsonObject(jsonObject.endDateOrDateTime), jsonObject.days.map(function (d) { return __WEBPACK_IMPORTED_MODULE_1__DayWrapper__["a" /* DayWrapper */].fromJsonObject(d); }), jsonObject.dosagePeriodPostfix)
            : undefined;
    };
    StructureWrapper.prototype.getIterationInterval = function () {
        return this.iterationInterval;
    };
    StructureWrapper.prototype.getSupplText = function () {
        return this.supplText;
    };
    StructureWrapper.prototype.getStartDateOrDateTime = function () {
        return this.startDateOrDateTime;
    };
    StructureWrapper.prototype.getEndDateOrDateTime = function () {
        return this.endDateOrDateTime;
    };
    StructureWrapper.prototype.getRefToSource = function () {
        return this.refToSource;
    };
    StructureWrapper.prototype.getDosagePeriodPostfix = function () {
        return this.dosagePeriodPostfix;
    };
    StructureWrapper.prototype.setDosagePeriodPostfix = function (v) {
        this.dosagePeriodPostfix = v;
    };
    StructureWrapper.prototype.startsAndEndsSameDay = function () {
        if (this.startDateOrDateTime && this.getEndDateOrDateTime()) {
            var startDate = this.startDateOrDateTime.getDateOrDateTime();
            var endDate = this.getEndDateOrDateTime().getDateOrDateTime();
            return startDate.getFullYear() === endDate.getFullYear()
                && startDate.getMonth() === endDate.getMonth()
                && startDate.getDate() === endDate.getDate();
        }
        else {
            return false;
        }
    };
    StructureWrapper.prototype.getDays = function () {
        return this.days;
    };
    StructureWrapper.prototype.getDay = function (dayNumber) {
        for (var _i = 0, _a = this.days; _i < _a.length; _i++) {
            var day = _a[_i];
            if (day.getDayNumber() === dayNumber)
                return day;
        }
        return undefined;
    };
    StructureWrapper.prototype.sameDayOfWeek = function () {
        if (this.getDays().length === 1) {
            return false;
        }
        var remainder = -1;
        for (var _i = 0, _a = this.getDays(); _i < _a.length; _i++) {
            var day = _a[_i];
            var r = day.getDayNumber() % 7;
            if (remainder >= 0 && remainder !== r)
                return false;
            remainder = r;
        }
        return true;
    };
    StructureWrapper.prototype.allDaysAreTheSame = function () {
        if (this._areAllDaysTheSame === undefined) {
            this._areAllDaysTheSame = true;
            var day0 = void 0;
            for (var _i = 0, _a = this.getDays(); _i < _a.length; _i++) {
                var day = _a[_i];
                if (day0) {
                    if (day0.getNumberOfDoses() !== day.getNumberOfDoses()) {
                        this._areAllDaysTheSame = false;
                        break;
                    }
                    else {
                        for (var d = 0; d < day0.getNumberOfDoses(); d++) {
                            if (!day0.getAllDoses()[d].theSameAs(day.getAllDoses()[d])) {
                                this._areAllDaysTheSame = false;
                                break;
                            }
                        }
                    }
                }
                else {
                    day0 = day;
                }
            }
        }
        return this._areAllDaysTheSame;
    };
    StructureWrapper.prototype.daysAreInUninteruptedSequenceFromOne = function () {
        var dayNumber = 1;
        for (var _i = 0, _a = this.getDays(); _i < _a.length; _i++) {
            var day = _a[_i];
            if (day.getDayNumber() !== dayNumber)
                return false;
            dayNumber++;
        }
        return true;
    };
    /**
     * Compares dosage quantities and the dosages label (the type of the dosage)
     * @return true if all dosages are of the same type and has the same quantity
     */
    StructureWrapper.prototype.allDosesAreTheSame = function () {
        if (this._areAllDosesTheSame === undefined) {
            this._areAllDosesTheSame = true;
            var dose0 = void 0;
            for (var _i = 0, _a = this.getDays(); _i < _a.length; _i++) {
                var day = _a[_i];
                for (var _b = 0, _c = day.getAllDoses(); _b < _c.length; _b++) {
                    var dose = _c[_b];
                    if (dose0 === undefined) {
                        dose0 = dose;
                    }
                    else if (!dose0.theSameAs(dose)) {
                        this._areAllDosesTheSame = false;
                        break;
                    }
                }
            }
        }
        return this._areAllDosesTheSame;
    };
    StructureWrapper.prototype.containsMorningNoonEveningNightDoses = function () {
        return this.getDays().some(function (d) { return d.containsMorningNoonEveningNightDoses(); });
    };
    StructureWrapper.prototype.containsPlainDose = function () {
        return this.getDays().some(function (d) { return d.containsPlainDose(); });
    };
    StructureWrapper.prototype.containsTimedDose = function () {
        return this.getDays().some(function (d) { return d.containsTimedDose(); });
    };
    StructureWrapper.prototype.containsAccordingToNeedDosesOnly = function () {
        return this.getDays().every(function (d) { return d.containsAccordingToNeedDosesOnly(); });
    };
    StructureWrapper.prototype.containsAccordingToNeedDose = function () {
        return this.getDays().some(function (d) { return d.containsAccordingToNeedDose(); });
    };
    StructureWrapper.prototype.getSumOfDoses = function () {
        var allSum;
        for (var _i = 0, _a = this.getDays(); _i < _a.length; _i++) {
            var day = _a[_i];
            var daySum = day.getSumOfDoses();
            if (allSum === undefined) {
                allSum = daySum;
            }
            else {
                allSum = { minimum: allSum.minimum + daySum.minimum, maximum: allSum.maximum + daySum.maximum };
            }
        }
        return allSum;
    };
    StructureWrapper.prototype.isEmpty = function () {
        return this.getDays().length === 0;
    };
    return StructureWrapper;
}());



/***/ }),
/* 14 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return XML140Generator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__AbstractXMLGenerator__ = __webpack_require__(49);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var XML140Generator = (function (_super) {
    __extends(XML140Generator, _super);
    function XML140Generator() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // Namespace to be used for all elements expect for Day elements in dosages, that due to the mixed namespaces in 1.4.2 has its own dosageNS parameter on many of the methods
    XML140Generator.prototype.getNamespace = function () {
        return "m12";
    };
    XML140Generator.prototype.generateXml = function (periods, unitTextSingular, unitTextPlural, supplementaryText) {
        var dosageElement = "<m12:Dosage " +
            "xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2012/06/01 ../../../2012/06/01/Dosage.xsd\" " +
            "xmlns:m12=\"http://www.dkma.dk/medicinecard/xml.schema/2012/06/01\" " +
            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
            "<m12:Structure>";
        var subElement;
        switch (periods[0].getType()) {
            case "M+M+A+N":
                subElement = this.generateMMANXml(periods[0].getIteration(), periods[0].getMapping(), unitTextSingular, unitTextPlural, supplementaryText, "m12", periods[0].getBeginDate(), periods[0].getEndDate());
                break;
            case "N daglig":
                subElement = this.generateDailyXml(periods[0].getIteration(), periods[0].getMapping(), unitTextSingular, unitTextPlural, supplementaryText, false, "m12", periods[0].getBeginDate(), periods[0].getEndDate());
                break;
            case "PN":
                subElement = this.generateDailyXml(periods[0].getIteration(), periods[0].getMapping(), unitTextSingular, unitTextPlural, supplementaryText, true, "m12", periods[0].getBeginDate(), periods[0].getEndDate());
                break;
            default:
                throw new Error("No support for type value '" + periods[0].getType() + "'");
        }
        return dosageElement + subElement + "</m12:Structure></m12:Dosage>";
    };
    XML140Generator.prototype.generateCommonXml = function (iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, beginDate, endDate) {
        var xml = "";
        if (iteration === 0) {
            xml += "<m12:NotIterated/>";
        }
        else {
            xml += "<m12:IterationInterval>" + iteration + "</m12:IterationInterval>";
        }
        xml += "<m12:StartDate>" + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].formatYYYYMMDD(beginDate) + "</m12:StartDate>" +
            "<m12:EndDate>" + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].formatYYYYMMDD(endDate) + "</m12:EndDate>" +
            "<m12:UnitTexts source=\"Doseringsforslag\">" +
            "<m12:Singular>" + this.escape(unitTextSingular) + "</m12:Singular>" +
            "<m12:Plural>" + this.escape(unitTextPlural) + "</m12:Plural>" +
            "</m12:UnitTexts>";
        if (supplementaryText && supplementaryText.length > 0) {
            xml += "<m12:SupplementaryText>" + this.escape(supplementaryText) + "</m12:SupplementaryText>";
        }
        return xml;
    };
    XML140Generator.prototype.generateMMANXml = function (iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, dosageNS, beginDate, endDate) {
        var mmanMapping = __WEBPACK_IMPORTED_MODULE_0__AbstractXMLGenerator__["a" /* AbstractXMLGenerator */].parseMapping(mapping);
        var xml = this.generateCommonXml(iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, beginDate, endDate);
        xml += "<" + this.getNamespace() + ":Day>" +
            "<" + dosageNS + ":Number>1</" + dosageNS + ":Number>";
        if (mmanMapping.getMorning()) {
            xml += "<" + dosageNS + ":Dose>"
                + "<" + dosageNS + ":Time>morning</" + dosageNS + ":Time>"
                + "<" + dosageNS + ":Quantity>" + mmanMapping.getMorning() + "</" + dosageNS + ":Quantity>"
                + "</" + dosageNS + ":Dose>";
        }
        if (mmanMapping.getNoon()) {
            xml += "<" + dosageNS + ":Dose>"
                + "<" + dosageNS + ":Time>noon</" + dosageNS + ":Time>"
                + "<" + dosageNS + ":Quantity>" + mmanMapping.getNoon() + "</" + dosageNS + ":Quantity>"
                + "</" + dosageNS + ":Dose>";
        }
        if (mmanMapping.getEvening()) {
            xml += "<" + dosageNS + ":Dose>"
                + "<" + dosageNS + ":Time>evening</" + dosageNS + ":Time>"
                + "<" + dosageNS + ":Quantity>" + mmanMapping.getEvening() + "</" + dosageNS + ":Quantity>"
                + "</" + dosageNS + ":Dose>";
        }
        if (mmanMapping.getNight()) {
            xml += "<" + dosageNS + ":Dose>"
                + "<" + dosageNS + ":Time>night</" + dosageNS + ":Time>"
                + "<" + dosageNS + ":Quantity>" + mmanMapping.getNight() + "</" + dosageNS + ":Quantity>"
                + "</" + dosageNS + ":Dose>";
        }
        xml += "</" + this.getNamespace() + ":Day>";
        return xml;
    };
    XML140Generator.prototype.generateDailyXml = function (iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, isPN, dosageNS, beginDate, endDate) {
        if (mapping.indexOf("dag ") >= 0) {
            return this.generateXmlForSeparateDays(iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, isPN, dosageNS, beginDate, endDate);
        }
        else {
            return this.generateXmlForSameDay(iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, isPN, dosageNS, beginDate, endDate);
        }
    };
    XML140Generator.prototype.generateXmlForSameDay = function (iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, isPN, dosageNS, beginDate, endDate) {
        var splittedMapping = mapping.split(";");
        var xml = this.generateCommonXml(iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, beginDate, endDate);
        xml += "<" + this.getNamespace() + ":Day>" +
            "<" + dosageNS + ":Number>1</" + dosageNS + ":Number>" +
            this.getQuantityString(splittedMapping, isPN, dosageNS) +
            "</" + this.getNamespace() + ":Day>";
        return xml;
    };
    XML140Generator.prototype.getQuantityString = function (quantities, isPN, dosageNS) {
        var xml = "";
        for (var _i = 0, quantities_1 = quantities; _i < quantities_1.length; _i++) {
            var dose = quantities_1[_i];
            xml += "<" + dosageNS + ":Dose><" + dosageNS + ":Quantity>" + dose + "</" + dosageNS + ":Quantity>";
            if (isPN) {
                xml += "<" + dosageNS + ":IsAccordingToNeed/>";
            }
            xml += "</" + dosageNS + ":Dose>";
        }
        return xml;
    };
    XML140Generator.prototype.generateXmlForSeparateDays = function (iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, isPN, dosageNS, beginDate, endDate) {
        var xml = this.generateCommonXml(iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, beginDate, endDate);
        var result;
        while ((result = this.daysMappingRegExp.exec(mapping)) != null) {
            var dayno = result[1];
            var quantity = result[2];
            xml += "<" + this.getNamespace() + ":Day><" + dosageNS + ":Number>" + dayno + "</" + dosageNS + ":Number>"
                + this.getQuantityString(quantity.split(";"), isPN, dosageNS)
                + "</" + this.getNamespace() + ":Day>";
        }
        return xml;
    };
    return XML140Generator;
}(__WEBPACK_IMPORTED_MODULE_0__AbstractXMLGenerator__["a" /* AbstractXMLGenerator */]));



/***/ }),
/* 15 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return XML142Generator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__ = __webpack_require__(5);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__XML140Generator__ = __webpack_require__(14);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var XML142Generator = (function (_super) {
    __extends(XML142Generator, _super);
    function XML142Generator() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // Namespace to be used for all elements expect for Day elements in dosages, that due to the mixed namespaces in 1.4.2 has its own dosageNS parameter on many of the methods
    XML142Generator.prototype.getNamespace = function () {
        return "m13";
    };
    XML142Generator.prototype.generateXml = function (periods, unitTextSingular, unitTextPlural, supplementaryText) {
        var dosageElement = "<m13:Dosage " +
            "xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2013/06/01 ../../../2013/06/01/DosageForRequest.xsd\" " +
            "xmlns:m13=\"http://www.dkma.dk/medicinecard/xml.schema/2013/06/01\" " +
            "xmlns:m12=\"http://www.dkma.dk/medicinecard/xml.schema/2012/06/01\" " +
            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
            "<m13:Structures>" +
            "<m13:UnitTexts source=\"Doseringsforslag\">" +
            "<m12:Singular>" + this.escape(unitTextSingular) + "</m12:Singular>" +
            "<m12:Plural>" + this.escape(unitTextPlural) + "</m12:Plural>" +
            "</m13:UnitTexts>";
        dosageElement += this.generateStructuresXml(periods, unitTextSingular, unitTextPlural, supplementaryText);
        return dosageElement + "</" + this.getNamespace() + ":Structures></" + this.getNamespace() + ":Dosage>";
    };
    XML142Generator.prototype.generateStructuresXml = function (periods, unitTextSingular, unitTextPlural, supplementaryText) {
        var _this = this;
        var dosageElement = "";
        var subElement;
        periods.forEach(function (p) {
            dosageElement += ("<" + _this.getNamespace() + ":Structure>");
            subElement = _this.generatePeriodXml(p, unitTextSingular, unitTextPlural, supplementaryText);
            dosageElement += subElement + "</" + _this.getNamespace() + ":Structure>";
        });
        return dosageElement;
    };
    XML142Generator.prototype.generatePeriodXml = function (period, unitTextSingular, unitTextPlural, supplementaryText) {
        switch (period.getType()) {
            case "M+M+A+N":
                return this.generateMMANXml(period.getIteration(), period.getMapping(), unitTextSingular, unitTextPlural, supplementaryText, "m12", period.getBeginDate(), period.getEndDate());
            case "N daglig":
                return this.generateDailyXml(period.getIteration(), period.getMapping(), unitTextSingular, unitTextPlural, supplementaryText, false, "m12", period.getBeginDate(), period.getEndDate());
            case "PN":
                return this.generateDailyXml(period.getIteration(), period.getMapping(), unitTextSingular, unitTextPlural, supplementaryText, true, "m12", period.getBeginDate(), period.getEndDate());
            default:
                throw new __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__["a" /* DosisTilTekstException */]("No support for type value '" + period.getType() + "'");
        }
    };
    XML142Generator.prototype.generateCommonXml = function (iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, beginDate, endDate) {
        var xml = "";
        if (iteration === 0) {
            xml += "<" + this.getNamespace() + ":NotIterated/>";
        }
        else {
            xml += "<" + this.getNamespace() + ":IterationInterval>" + iteration + "</" + this.getNamespace() + ":IterationInterval>";
        }
        xml += "<" + this.getNamespace() + ":StartDate>" + __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].formatYYYYMMDD(beginDate) + "</" + this.getNamespace() + ":StartDate>" +
            "<" + this.getNamespace() + ":EndDate>" + __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].formatYYYYMMDD(endDate) + "</" + this.getNamespace() + ":EndDate>";
        if (supplementaryText && supplementaryText.length > 0) {
            xml += "<" + this.getNamespace() + ":SupplementaryText>" + this.escape(supplementaryText) + "</" + this.getNamespace() + ":SupplementaryText>";
        }
        return xml;
    };
    return XML142Generator;
}(__WEBPACK_IMPORTED_MODULE_1__XML140Generator__["a" /* XML140Generator */]));



/***/ }),
/* 16 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return XML144Generator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__ = __webpack_require__(5);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__XML142Generator__ = __webpack_require__(15);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var XML144Generator = (function (_super) {
    __extends(XML144Generator, _super);
    function XML144Generator() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // Namespace to be used for all elements expect for Day elements in dosages, that due to the mixed namespaces in 1.4.2 has its own dosageNS parameter on many of the methods
    XML144Generator.prototype.getNamespace = function () {
        return "m15";
    };
    XML144Generator.prototype.generateXml = function (periods, unitTextSingular, unitTextPlural, supplementaryText) {
        var dosageElement = "<m15:Dosage " +
            "xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2015/01/01 ../../../2015/01/01/DosageForRequest.xsd\" " +
            "xmlns:m15=\"http://www.dkma.dk/medicinecard/xml.schema/2015/01/01\" " +
            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
            "<m15:Structures>" +
            "<m15:UnitTexts source=\"Doseringsforslag\">" +
            "<m15:Singular>" + this.escape(unitTextSingular) + "</m15:Singular>" +
            "<m15:Plural>" + this.escape(unitTextPlural) + "</m15:Plural>" +
            "</m15:UnitTexts>";
        dosageElement += this.generateStructuresXml(periods, unitTextSingular, unitTextPlural, supplementaryText);
        return dosageElement + "</" + this.getNamespace() + ":Structures></" + this.getNamespace() + ":Dosage>";
    };
    XML144Generator.prototype.generatePeriodXml = function (period, unitTextSingular, unitTextPlural, supplementaryText) {
        switch (period.getType()) {
            case "M+M+A+N":
                return this.generateMMANXml(period.getIteration(), period.getMapping(), unitTextSingular, unitTextPlural, supplementaryText, this.getNamespace(), period.getBeginDate(), period.getEndDate());
            case "N daglig":
                return this.generateDailyXml(period.getIteration(), period.getMapping(), unitTextSingular, unitTextPlural, supplementaryText, false, this.getNamespace(), period.getBeginDate(), period.getEndDate());
            case "PN":
                return this.generateDailyXml(period.getIteration(), period.getMapping(), unitTextSingular, unitTextPlural, supplementaryText, true, this.getNamespace(), period.getBeginDate(), period.getEndDate());
            default:
                throw new __WEBPACK_IMPORTED_MODULE_0__DosisTilTekstException__["a" /* DosisTilTekstException */]("No support for type value '" + period.getType() + "'");
        }
    };
    return XML144Generator;
}(__WEBPACK_IMPORTED_MODULE_1__XML142Generator__["a" /* XML142Generator */]));



/***/ }),
/* 17 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SimpleLongTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var SimpleLongTextConverterImpl = (function (_super) {
    __extends(SimpleLongTextConverterImpl, _super);
    function SimpleLongTextConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SimpleLongTextConverterImpl.prototype.convert = function (text, startDateOrDateTime, endDateOrDateTime) {
        var s = "";
        if (startDateOrDateTime && endDateOrDateTime && startDateOrDateTime.isEqualTo(endDateOrDateTime)) {
            // Same day dosage
            s += "Doseringen foretages kun " + this.datesToLongText(endDateOrDateTime) + ".\n" + "   Dosering:\n   ";
        }
        else if (startDateOrDateTime) {
            s += this.getDosageStartText(startDateOrDateTime);
            if (endDateOrDateTime) {
                s += " og ophører " + this.datesToLongText(endDateOrDateTime) + ".\n" + "   Doseringsforløb:\n   ";
            }
            else {
                s += ".\n   Doseringsforløb:\n   ";
            }
        }
        else if (!startDateOrDateTime) {
            if (endDateOrDateTime) {
                s += "Doseringsforløbet ophører " + this.datesToLongText(endDateOrDateTime) + ".\n" + "   Doseringsforløb:\n   ";
            }
        }
        s += text;
        return s;
    };
    return SimpleLongTextConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 18 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SimpleLimitedAccordingToNeedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


/**
 * Conversion of simple but limited "according to need" dosage, with or without suppl. dosage free text
 * <p>
 * Example:<br>
 * 283: 1 pust ved anfald højst 3 gange daglig
 */
var SimpleLimitedAccordingToNeedConverterImpl = (function (_super) {
    __extends(SimpleLimitedAccordingToNeedConverterImpl, _super);
    function SimpleLimitedAccordingToNeedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SimpleLimitedAccordingToNeedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.getDayNumber() !== 1)
            return false;
        if (!day.containsAccordingToNeedDosesOnly())
            return false;
        if (!day.allDosesAreTheSame())
            return false;
        return true;
    };
    SimpleLimitedAccordingToNeedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAccordingToNeedDoses()[0], dosage.structures.getUnitOrUnits());
        text += " efter behov";
        if (day.getNumberOfAccordingToNeedDoses() === 1)
            text += ", højst " + day.getNumberOfAccordingToNeedDoses() + " gang daglig";
        else
            text += ", højst " + day.getNumberOfAccordingToNeedDoses() + " gange daglig";
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text.toString();
    };
    return SimpleLimitedAccordingToNeedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 19 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EveningDoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DoseWrapper__ = __webpack_require__(4);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var EveningDoseWrapper = (function (_super) {
    __extends(EveningDoseWrapper, _super);
    function EveningDoseWrapper(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, doseQuantityString, minimalDoseQuantityString, maximalDoseQuantityString, isAccordingToNeed) {
        return _super.call(this, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) || this;
    }
    EveningDoseWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new EveningDoseWrapper(jsonObject.doseQuantity, jsonObject.minimalDoseQuantity, jsonObject.maximalDoseQuantity, jsonObject.doseQuantityString, jsonObject.minimalDoseQuantityString, jsonObject.maximalDoseQuantityString, jsonObject.isAccordingToNeed)
            : undefined;
    };
    EveningDoseWrapper.prototype.getLabel = function () {
        return EveningDoseWrapper.LABEL;
    };
    EveningDoseWrapper.LABEL = "aften";
    return EveningDoseWrapper;
}(__WEBPACK_IMPORTED_MODULE_0__DoseWrapper__["a" /* DoseWrapper */]));



/***/ }),
/* 20 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return FreeTextWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__ = __webpack_require__(6);

var FreeTextWrapper = (function () {
    function FreeTextWrapper(startDateOrDateTime, endDateOrDateTime, text) {
        this.startDateOrDateTime = startDateOrDateTime;
        this.endDateOrDateTime = endDateOrDateTime;
        this.text = text;
    }
    FreeTextWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ? new FreeTextWrapper(__WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */].fromJsonObject(jsonObject.startDateOrDateTime), __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */].fromJsonObject(jsonObject.endDateOrDateTime), jsonObject.text) : undefined;
    };
    FreeTextWrapper.makeFreeText = function (startDateOrDateTime, endDateOrDateTime, text) {
        return new FreeTextWrapper(startDateOrDateTime, endDateOrDateTime, text);
    };
    FreeTextWrapper.prototype.getStartDateOrDateTime = function () {
        return this.startDateOrDateTime;
    };
    FreeTextWrapper.prototype.getEndDateOrDateTime = function () {
        return this.endDateOrDateTime;
    };
    FreeTextWrapper.prototype.getText = function () {
        return this.text;
    };
    return FreeTextWrapper;
}());



/***/ }),
/* 21 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MorningDoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DoseWrapper__ = __webpack_require__(4);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var MorningDoseWrapper = (function (_super) {
    __extends(MorningDoseWrapper, _super);
    function MorningDoseWrapper(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, doseQuantitystring, minimalDoseQuantitystring, maximalDoseQuantitystring, isAccordingToNeed) {
        return _super.call(this, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) || this;
    }
    MorningDoseWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new MorningDoseWrapper(jsonObject.doseQuantity, jsonObject.minimalDoseQuantity, jsonObject.maximalDoseQuantity, jsonObject.doseQuantityString, jsonObject.minimalDoseQuantityString, jsonObject.maximalDoseQuantityString, jsonObject.isAccordingToNeed)
            : undefined;
    };
    MorningDoseWrapper.prototype.getLabel = function () {
        return MorningDoseWrapper.LABEL;
    };
    MorningDoseWrapper.LABEL = "morgen";
    return MorningDoseWrapper;
}(__WEBPACK_IMPORTED_MODULE_0__DoseWrapper__["a" /* DoseWrapper */]));



/***/ }),
/* 22 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NightDoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DoseWrapper__ = __webpack_require__(4);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var NightDoseWrapper = (function (_super) {
    __extends(NightDoseWrapper, _super);
    function NightDoseWrapper(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, doseQuantitystring, minimalDoseQuantitystring, maximalDoseQuantitystring, isAccordingToNeed) {
        return _super.call(this, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) || this;
    }
    NightDoseWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new NightDoseWrapper(jsonObject.doseQuantity, jsonObject.minimalDoseQuantity, jsonObject.maximalDoseQuantity, jsonObject.doseQuantityString, jsonObject.minimalDoseQuantityString, jsonObject.maximalDoseQuantityString, jsonObject.isAccordingToNeed)
            : undefined;
    };
    NightDoseWrapper.prototype.getLabel = function () {
        return NightDoseWrapper.LABEL;
    };
    NightDoseWrapper.LABEL = "før sengetid";
    return NightDoseWrapper;
}(__WEBPACK_IMPORTED_MODULE_0__DoseWrapper__["a" /* DoseWrapper */]));



/***/ }),
/* 23 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NoonDoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DoseWrapper__ = __webpack_require__(4);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var NoonDoseWrapper = (function (_super) {
    __extends(NoonDoseWrapper, _super);
    function NoonDoseWrapper(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, doseQuantitystring, minimalDoseQuantitystring, maximalDoseQuantitystring, isAccordingToNeed) {
        return _super.call(this, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) || this;
    }
    NoonDoseWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new NoonDoseWrapper(jsonObject.doseQuantity, jsonObject.minimalDoseQuantity, jsonObject.maximalDoseQuantity, jsonObject.doseQuantityString, jsonObject.minimalDoseQuantityString, jsonObject.maximalDoseQuantityString, jsonObject.isAccordingToNeed)
            : undefined;
    };
    NoonDoseWrapper.prototype.getLabel = function () {
        return NoonDoseWrapper.LABEL;
    };
    NoonDoseWrapper.LABEL = "middag";
    return NoonDoseWrapper;
}(__WEBPACK_IMPORTED_MODULE_0__DoseWrapper__["a" /* DoseWrapper */]));



/***/ }),
/* 24 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PlainDoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DoseWrapper__ = __webpack_require__(4);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var PlainDoseWrapper = (function (_super) {
    __extends(PlainDoseWrapper, _super);
    function PlainDoseWrapper(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, doseQuantityString, minimalDoseQuantityString, maximalDoseQuantityString, isAccordingToNeed) {
        return _super.call(this, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) || this;
    }
    PlainDoseWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new PlainDoseWrapper(jsonObject.doseQuantity, jsonObject.minimalDoseQuantity, jsonObject.maximalDoseQuantity, jsonObject.doseQuantityString, jsonObject.minimalDoseQuantityString, jsonObject.maximalDoseQuantityString, jsonObject.isAccordingToNeed)
            : undefined;
    };
    PlainDoseWrapper.prototype.getLabel = function () {
        return PlainDoseWrapper.LABEL;
    };
    PlainDoseWrapper.LABEL = "";
    return PlainDoseWrapper;
}(__WEBPACK_IMPORTED_MODULE_0__DoseWrapper__["a" /* DoseWrapper */]));



/***/ }),
/* 25 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UnitOrUnitsWrapper; });
var UnitOrUnitsWrapper = (function () {
    function UnitOrUnitsWrapper(unit, unitSingular, unitPlural) {
        this.unit = unit;
        this.unitSingular = unitSingular;
        this.unitPlural = unitPlural;
    }
    UnitOrUnitsWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new UnitOrUnitsWrapper(jsonObject.unit, jsonObject.unitSingular, jsonObject.unitPlural)
            : undefined;
    };
    UnitOrUnitsWrapper.prototype.getUnit = function () {
        return this.unit;
    };
    UnitOrUnitsWrapper.prototype.getUnitSingular = function () {
        return this.unitSingular;
    };
    UnitOrUnitsWrapper.prototype.getUnitPlural = function () {
        return this.unitPlural;
    };
    return UnitOrUnitsWrapper;
}());



/***/ }),
/* 26 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DailyDosisCalculator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__DailyDosis__ = __webpack_require__(57);


/**
 * Class for calculating the avg. daily dosis from the dosage structure.
 * This is possible when:
 * - The dosage is given in structured form (not "in local system" or free text)
 * - The dosage is for one periode
 * - The free text in the dosage doesn't alter the dosage expressed in dosage value and unit
 *   (doing so is not allowed according to the business rules, but this cannot be validated).
 * - And the dosage doesn't contain according to need dosages
 */
var DailyDosisCalculator = (function () {
    function DailyDosisCalculator() {
    }
    DailyDosisCalculator.calculate = function (dosageJson) {
        return DailyDosisCalculator.calculateWrapper(__WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson));
    };
    DailyDosisCalculator.calculateStr = function (jsonStr) {
        return DailyDosisCalculator.calculate(JSON.parse(jsonStr));
    };
    DailyDosisCalculator.calculateWrapper = function (dosage) {
        if (dosage.isAdministrationAccordingToSchema())
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, undefined, undefined);
        else if (dosage.isFreeText())
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, undefined, undefined);
        else
            return DailyDosisCalculator.calculateFromStructures(dosage.structures);
    };
    DailyDosisCalculator.calculateFromStructures = function (structures) {
        if (structures.getStructures().length === 1 && structures.getStructures()[0].getDays() !== undefined && structures.getStructures()[0].getDays().length > 0)
            return DailyDosisCalculator.calculateFromStructure(structures.getStructures()[0], structures.getUnitOrUnits());
        else
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, undefined, undefined); // Calculating a daily dosis for more than one dosage periode is not supported
    };
    DailyDosisCalculator.calculateFromStructure = function (structure, unitOrUnits) {
        // If the dosage isn't repeated it doesn't make sense to calculate an average
        // unless all daily doses are equal
        if (structure.getIterationInterval() === 0)
            if (!structure.allDaysAreTheSame())
                return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, undefined, undefined);
        // If the structured dosage contains any doses according to need
        // we cannot calculate an average dosis
        if (structure.containsAccordingToNeedDose())
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, undefined, undefined);
        // If there is a dosage for day zero (meaning not related to a specific day)
        // we cannot calculate an average dosis
        if (structure.getDay(0) !== undefined)
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, undefined, undefined);
        // Otherwise we will calculate an average dosage.
        // If the iteration interval is zero, the dosage is not repeated. This means
        // that the dosage for each day is given.
        if (structure.getIterationInterval() === 0)
            return DailyDosisCalculator.calculateAvg(structure.getSumOfDoses(), structure.getDays()[structure.getDays().length - 1].getDayNumber(), unitOrUnits);
        else
            return DailyDosisCalculator.calculateAvg(structure.getSumOfDoses(), structure.getIterationInterval(), unitOrUnits);
    };
    DailyDosisCalculator.calculateAvg = function (sum, divisor, unitOrUnits) {
        var avg = {
            minimum: parseFloat((sum.minimum / divisor).toFixed(9)),
            maximum: parseFloat((sum.maximum / divisor).toFixed(9))
        };
        if (avg.maximum - avg.minimum < 0.000000001)
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](avg.minimum, undefined, unitOrUnits);
        else
            return new __WEBPACK_IMPORTED_MODULE_1__DailyDosis__["a" /* DailyDosis */](undefined, { minimum: avg.minimum, maximum: avg.maximum }, unitOrUnits);
    };
    return DailyDosisCalculator;
}());



/***/ }),
/* 27 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosagePeriod; });
var DosagePeriod = (function () {
    function DosagePeriod(type, mapping, iteration, beginDate, endDate) {
        this.type = type;
        this.mapping = mapping;
        this.iteration = iteration;
        this.beginDate = beginDate;
        this.endDate = endDate;
    }
    DosagePeriod.prototype.getType = function () {
        return this.type;
    };
    DosagePeriod.prototype.getMapping = function () {
        return this.mapping;
    };
    DosagePeriod.prototype.getIteration = function () {
        return this.iteration;
    };
    DosagePeriod.prototype.getBeginDate = function () {
        return this.beginDate;
    };
    DosagePeriod.prototype.getEndDate = function () {
        return this.endDate;
    };
    return DosagePeriod;
}());



/***/ }),
/* 28 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosageProposalXML; });
var DosageProposalXML = (function () {
    function DosageProposalXML(xml, shortDosageTranslation, longDosageTranslation) {
        this._xml = xml;
        this._shortDosageTranslation = shortDosageTranslation;
        this._longDosageTranslation = longDosageTranslation;
    }
    DosageProposalXML.prototype.getXml = function () {
        return this._xml;
    };
    DosageProposalXML.prototype.getShortDosageTranslation = function () {
        return this._shortDosageTranslation;
    };
    DosageProposalXML.prototype.getLongDosageTranslation = function () {
        return this._longDosageTranslation;
    };
    return DosageProposalXML;
}());



/***/ }),
/* 29 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return XML146Generator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__XML144Generator__ = __webpack_require__(16);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var XML146Generator = (function (_super) {
    __extends(XML146Generator, _super);
    function XML146Generator() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // Namespace to be used for all elements expect for Day elements in dosages, that due to the mixed namespaces in 1.4.2 has its own dosageNS parameter on many of the methods
    XML146Generator.prototype.getNamespace = function () {
        return "m16";
    };
    XML146Generator.prototype.generateXml = function (periods, unitTextSingular, unitTextPlural, supplementaryText) {
        var dosageElement = "<m16:Dosage " +
            "xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01 ../../../2015/06/01/DosageForRequest.xsd\" " +
            "xmlns:m16=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01\" " +
            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
            "<m16:UnitTexts source=\"Doseringsforslag\">" +
            "<m16:Singular>" + this.escape(unitTextSingular) + "</m16:Singular>" +
            "<m16:Plural>" + this.escape(unitTextPlural) + "</m16:Plural>" +
            "</m16:UnitTexts>";
        var fixedPeriods = periods.filter(function (p) { return p.getType() !== "PN"; });
        var pnPeriods = periods.filter(function (p) { return p.getType() === "PN"; });
        if (fixedPeriods.length > 0) {
            dosageElement += "<" + this.getNamespace() + ":StructuresFixed>";
            dosageElement += this.generateStructuresXml(fixedPeriods, unitTextSingular, unitTextPlural, supplementaryText);
            dosageElement += "</" + this.getNamespace() + ":StructuresFixed>";
        }
        if (pnPeriods.length > 0) {
            dosageElement += "<" + this.getNamespace() + ":StructuresAccordingToNeed>";
            dosageElement += this.generateStructuresXml(pnPeriods, unitTextSingular, unitTextPlural, supplementaryText);
            dosageElement += "</" + this.getNamespace() + ":StructuresAccordingToNeed>";
        }
        return dosageElement + "</" + this.getNamespace() + ":Dosage>";
    };
    XML146Generator.prototype.getQuantityString = function (quantities, isPN, dosageNS) {
        var xml = "";
        for (var _i = 0, quantities_1 = quantities; _i < quantities_1.length; _i++) {
            var dose = quantities_1[_i];
            xml += "<" + dosageNS + ":Dose><" + dosageNS + ":Quantity>" + dose + "</" + dosageNS + ":Quantity>" + "</" + dosageNS + ":Dose>";
        }
        return xml;
    };
    return XML146Generator;
}(__WEBPACK_IMPORTED_MODULE_0__XML144Generator__["a" /* XML144Generator */]));



/***/ }),
/* 30 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosageTypeCalculator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__DosageType__ = __webpack_require__(50);


var DosageTypeCalculator = (function () {
    function DosageTypeCalculator() {
    }
    DosageTypeCalculator.calculate = function (dosageJson) {
        return DosageTypeCalculator.calculateWrapper(__WEBPACK_IMPORTED_MODULE_0__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson));
    };
    DosageTypeCalculator.calculateStr = function (jsonStr) {
        return DosageTypeCalculator.calculate(JSON.parse(jsonStr));
    };
    DosageTypeCalculator.calculateWrapper = function (dosage) {
        if (dosage.isAdministrationAccordingToSchema())
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].Unspecified;
        else if (dosage.isFreeText())
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].Unspecified;
        else
            return DosageTypeCalculator.calculateFromStructures(dosage.structures);
    };
    DosageTypeCalculator.calculateFromStructures = function (structures) {
        if (structures.getStructures().length === 1 || DosageTypeCalculator.allStructuresHasSameDosageType(structures)) {
            return DosageTypeCalculator.calculateFromStructure(structures.getStructures()[0]);
        }
        else {
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].Combined;
        }
    };
    DosageTypeCalculator.allStructuresHasSameDosageType = function (structures) {
        if (structures && structures.getStructures()) {
            for (var i = 0; i < structures.getStructures().length; i++) {
                var firstType = DosageTypeCalculator.calculateFromStructure(structures.getStructures()[i]);
                for (var j = i + 1; j < structures.getStructures().length; j++) {
                    if (firstType !== DosageTypeCalculator.calculateFromStructure(structures.getStructures()[j])) {
                        return false;
                    }
                }
            }
        }
        return true;
    };
    DosageTypeCalculator.calculateFromStructure = function (structure) {
        if (DosageTypeCalculator.isAccordingToNeed(structure))
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].AccordingToNeed;
        else if (DosageTypeCalculator.isOneTime(structure))
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].OneTime;
        else if (DosageTypeCalculator.isTemporary(structure))
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].Temporary;
        else if (DosageTypeCalculator.isFixed(structure))
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].Fixed;
        else
            return __WEBPACK_IMPORTED_MODULE_1__DosageType__["a" /* DosageType */].Combined;
    };
    DosageTypeCalculator.isAccordingToNeed = function (structure) {
        // If the dosage contains only according to need doses, it is quite simply just
        // an according to need dosage
        return structure.containsAccordingToNeedDosesOnly();
    };
    DosageTypeCalculator.isTemporary = function (structure) {
        // If there is no end date defined the dosage must not be iterated
        if (structure.getEndDateOrDateTime() === undefined && structure.getIterationInterval() > 0)
            return false;
        // If there is an according to need dose in the dosage it is not a (clean)
        // temporary dosage.
        if (structure.containsAccordingToNeedDose())
            return false;
        return true;
    };
    DosageTypeCalculator.isFixed = function (structure) {
        // If there is an end date defined the dosage isn't fixed
        if (structure.getEndDateOrDateTime() !== undefined)
            return false;
        // If the dosage isn't iterated it isn't fixed
        if (structure.getIterationInterval() === 0)
            return false;
        // If there is an according to need dose in the dosage it is not a (clean)
        // temporary dosage.
        if (structure.containsAccordingToNeedDose())
            return false;
        return true;
    };
    DosageTypeCalculator.isOneTime = function (structure) {
        var isSameDayDateInterval = structure.startsAndEndsSameDay();
        // If we have and end date it must be the same day as the start date
        if (structure.getEndDateOrDateTime() !== undefined && !isSameDayDateInterval)
            return false;
        // We don't want to have a day 0 defined, as it contains only meaningful information
        // if the dosage is given according to need
        if (structure.getDay(0))
            return false;
        // The dose must be defined for day 1
        var day = structure.getDay(1);
        if (day == null)
            return false;
        // There must be exactly one dose
        if (day.getAllDoses().length !== 1)
            return false;
        // And the dose must not be according to need
        if (day.containsAccordingToNeedDose())
            return false;
        // If the dosage isn't iterated we are happy now
        if (structure.getIterationInterval() === 0)
            return true;
        // If the dosage is iterated the end date must be defined as the same day as the start day
        return isSameDayDateInterval;
    };
    return DosageTypeCalculator;
}());



/***/ }),
/* 31 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LocalTime; });
var LocalTime = (function () {
    function LocalTime(hour, minute, second) {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }
    LocalTime.prototype.getMinute = function () {
        return this.minute;
    };
    LocalTime.prototype.getSecond = function () {
        return this.second;
    };
    LocalTime.prototype.getHour = function () {
        return this.hour;
    };
    LocalTime.fromJsonObject = function (jsonObject) {
        return new LocalTime(jsonObject.hour, jsonObject.minute, jsonObject.second);
    };
    LocalTime.prototype.toString = function () {
        var hourString = this.hour.toString();
        if (hourString.length === 1) {
            hourString = "0" + hourString;
        }
        var minuteString = this.minute.toString();
        if (minuteString.length === 1) {
            minuteString = "0" + minuteString;
        }
        var secondString;
        if (this.second !== undefined) {
            secondString = this.second.toString();
            if (secondString.length === 1) {
                secondString = "0" + secondString;
            }
        }
        var s = hourString + ":" + minuteString;
        if (secondString) {
            s += ":" + secondString;
        }
        return s;
    };
    return LocalTime;
}());



/***/ }),
/* 32 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Validator; });
var Validator = (function () {
    function Validator() {
    }
    Validator.validate = function (dosage) {
        return;
    };
    return Validator;
}());



/***/ }),
/* 33 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AdministrationAccordingToSchemaConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__SimpleLongTextConverterImpl__ = __webpack_require__(17);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var AdministrationAccordingToSchemaConverterImpl = (function (_super) {
    __extends(AdministrationAccordingToSchemaConverterImpl, _super);
    function AdministrationAccordingToSchemaConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    AdministrationAccordingToSchemaConverterImpl.prototype.canConvert = function (dosage) {
        return dosage.isAdministrationAccordingToSchema();
    };
    AdministrationAccordingToSchemaConverterImpl.prototype.doConvert = function (dosage) {
        return this.convert("Dosering efter skriftlig anvisning", dosage.administrationAccordingToSchema.getStartDateOrDateTime(), dosage.administrationAccordingToSchema.getEndDateOrDateTime());
    };
    return AdministrationAccordingToSchemaConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__SimpleLongTextConverterImpl__["a" /* SimpleLongTextConverterImpl */]));



/***/ }),
/* 34 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DailyRepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var DailyRepeatedConverterImpl = (function (_super) {
    __extends(DailyRepeatedConverterImpl, _super);
    function DailyRepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    DailyRepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (!dosage.structures)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
            return false;
        if (structure.getDays().length !== 1)
            return false;
        if (structure.getDays()[0].getDayNumber() !== 1)
            return false;
        return true;
    };
    DailyRepeatedConverterImpl.prototype.doConvert = function (dosage) {
        return this.convert(dosage.structures.getUnitOrUnits(), dosage.structures.getStructures()[0]);
    };
    DailyRepeatedConverterImpl.prototype.convert = function (unitOrUnits, structure) {
        var s = "";
        s += this.getDosageStartText(structure.getStartDateOrDateTime());
        if (structure.getEndDateOrDateTime()) {
            s += ", gentages hver dag";
            s += this.getDosageEndText(structure.getEndDateOrDateTime());
            s += ":\n";
        }
        else {
            s += " og gentages hver dag:\n";
        }
        s += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].INDENT + "Doseringsforløb:\n";
        s += this.getDaysText(unitOrUnits, structure);
        return s.toString();
    };
    return DailyRepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 35 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DefaultLongTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var DefaultLongTextConverterImpl = (function (_super) {
    __extends(DefaultLongTextConverterImpl, _super);
    function DefaultLongTextConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    DefaultLongTextConverterImpl.prototype.canConvert = function (dosageStructure) {
        // The default converter must handle all cases with a single periode, to ensure that we always create a long
        // dosage text. This converter is added last in the LongTextConverters list of possible
        // converters.
        return dosageStructure.structures.getStructures().length === 1;
    };
    DefaultLongTextConverterImpl.prototype.doConvert = function (dosage) {
        return this.convert(dosage.structures.getUnitOrUnits(), dosage.structures.getStructures()[0]);
    };
    DefaultLongTextConverterImpl.prototype.convert = function (unitOrUnits, structure) {
        var s = "";
        if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime())) {
            // Same day dosage
            s += "Doseringen foretages kun " + this.datesToLongText(structure.getStartDateOrDateTime()) + ":\n";
        }
        else if (structure.getIterationInterval() === 0) {
            // Not repeated dosage
            s += this.getDosageStartText(structure.getStartDateOrDateTime());
            // If there is just one day with according to need dosages we don't want say when to stop
            if (structure.getDays().length === 1 && structure.containsAccordingToNeedDosesOnly()) {
                s += ":\n";
            }
            else {
                if (structure.getEndDateOrDateTime()) {
                    s += this.getDosageEndText(structure.getEndDateOrDateTime());
                }
                else {
                    s += " og ophører efter det angivne forløb";
                }
                s += this.getNoteText(structure);
            }
        }
        else if (structure.getIterationInterval() === 1) {
            // Daily dosage
            s += this.getDosageStartText(structure.getStartDateOrDateTime());
            if (structure.getEndDateOrDateTime()) {
                s += ", gentages hver dag";
                s += this.getDosageEndText(structure.getEndDateOrDateTime());
                s += ":\n";
            }
            else {
                s += " og gentages hver dag:\n";
            }
        }
        else if (structure.getIterationInterval() > 1) {
            // Dosage repeated after more than one day
            s += this.getDosageStartText(structure.getStartDateOrDateTime());
            s = this.appendRepetition(s, structure);
            if (structure.getEndDateOrDateTime()) {
                s += this.getDosageEndText(structure.getEndDateOrDateTime());
            }
            s += this.getNoteText(structure);
        }
        s += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].INDENT + "Doseringsforløb:\n";
        s += this.getDaysText(unitOrUnits, structure);
        return s;
    };
    DefaultLongTextConverterImpl.prototype.appendRepetition = function (s, structure) {
        return s + ", forløbet gentages efter " + structure.getIterationInterval() + " dage";
    };
    return DefaultLongTextConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 36 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DefaultMultiPeriodeLongTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__vowrapper_StructuresWrapper__ = __webpack_require__(7);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var DefaultMultiPeriodeLongTextConverterImpl = (function (_super) {
    __extends(DefaultMultiPeriodeLongTextConverterImpl, _super);
    function DefaultMultiPeriodeLongTextConverterImpl(longTextConverter) {
        var _this = _super.call(this) || this;
        _this.longTextConverter = longTextConverter;
        return _this;
    }
    DefaultMultiPeriodeLongTextConverterImpl.prototype.canConvert = function (dosageStructure) {
        if (dosageStructure.structures) {
            return dosageStructure.structures.getStructures().length > 1;
        }
        return false;
    };
    DefaultMultiPeriodeLongTextConverterImpl.prototype.doConvert = function (dosage) {
        var _this = this;
        var s = "Doseringen indeholder flere perioder";
        if (dosage.structures.hasOverlappingPeriodes()) {
            s += ", bemærk at der er overlappende perioder";
        }
        s += ":\n\n";
        dosage.structures.getStructures().forEach(function (structure) {
            var w = __WEBPACK_IMPORTED_MODULE_1__vowrapper_DosageWrapper__["a" /* DosageWrapper */].makeStructuredDosage(new __WEBPACK_IMPORTED_MODULE_2__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](dosage.structures.getUnitOrUnits(), [structure]));
            s += (_this.longTextConverter.convertWrapper(w) + "\n\n");
        });
        return s.trim();
    };
    return DefaultMultiPeriodeLongTextConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 37 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EmptyStructureConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var EmptyStructureConverterImpl = (function (_super) {
    __extends(EmptyStructureConverterImpl, _super);
    function EmptyStructureConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    EmptyStructureConverterImpl.prototype.canConvert = function (dosageStructure) {
        return dosageStructure.isStructured()
            && dosageStructure.structures.getStructures()
            && dosageStructure.structures.getStructures().length === 1
            && dosageStructure.structures.getStructures()[0].getDays().length === 0;
    };
    EmptyStructureConverterImpl.prototype.doConvert = function (dosageStructure) {
        var s = "Doseringsforløbet starter " + this.datesToLongText(dosageStructure.structures.getStructures()[0].getStartDateOrDateTime());
        if (dosageStructure.structures.getStructures()[0].getEndDateOrDateTime()) {
            s += " og ophører " + this.datesToLongText(dosageStructure.structures.getStructures()[0].getEndDateOrDateTime());
        }
        s += ":\n" + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].INDENT + "Bemærk: skal ikke anvendes i denne periode!\n";
        return s;
    };
    return EmptyStructureConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 38 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return FreeTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__SimpleLongTextConverterImpl__ = __webpack_require__(17);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var FreeTextConverterImpl = (function (_super) {
    __extends(FreeTextConverterImpl, _super);
    function FreeTextConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    FreeTextConverterImpl.prototype.canConvert = function (dosage) {
        return dosage.freeText !== undefined && dosage.freeText !== null;
    };
    FreeTextConverterImpl.prototype.doConvert = function (dosage) {
        return this.convert(dosage.freeText.getText(), dosage.freeText.getStartDateOrDateTime(), dosage.freeText.getEndDateOrDateTime());
    };
    return FreeTextConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__SimpleLongTextConverterImpl__["a" /* SimpleLongTextConverterImpl */]));



/***/ }),
/* 39 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TwoDaysRepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var TwoDaysRepeatedConverterImpl = (function (_super) {
    __extends(TwoDaysRepeatedConverterImpl, _super);
    function TwoDaysRepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    TwoDaysRepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures) {
            if (dosage.structures.getStructures().length !== 1)
                return false;
            var structure = dosage.structures.getStructures()[0];
            if (structure.getIterationInterval() !== 2)
                return false;
            if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
                return false;
            if (structure.getDays().length > 2)
                return false;
            if (structure.getDays().length === 1)
                if (structure.getDays()[0].getDayNumber() !== 1 && structure.getDays()[0].getDayNumber() !== 2)
                    return false;
            if (structure.getDays().length === 2)
                if (structure.getDays()[0].getDayNumber() !== 1 || structure.getDays()[1].getDayNumber() !== 2)
                    return false;
            return true;
        }
        return false;
    };
    TwoDaysRepeatedConverterImpl.prototype.doConvert = function (dosage) {
        return this.convert(dosage.structures.getUnitOrUnits(), dosage.structures.getStructures()[0]);
    };
    TwoDaysRepeatedConverterImpl.prototype.convert = function (unitOrUnits, structure) {
        var s = this.getDosageStartText(structure.getStartDateOrDateTime());
        s += ", forløbet gentages hver 2. dag";
        if (structure.getEndDateOrDateTime()) {
            s += this.getDosageEndText(structure.getEndDateOrDateTime());
        }
        s += this.getNoteText(structure);
        s += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].INDENT + "Doseringsforløb:\n";
        s += this.getDaysText(unitOrUnits, structure);
        return s.toString();
    };
    TwoDaysRepeatedConverterImpl.prototype.makeDaysLabel = function (dosageStructure, day) {
        return "Dag " + day.getDayNumber() + ": ";
    };
    return TwoDaysRepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__LongTextConverterImpl__["a" /* LongTextConverterImpl */]));



/***/ }),
/* 40 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AdministrationAccordingToSchemaConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var AdministrationAccordingToSchemaConverterImpl = (function (_super) {
    __extends(AdministrationAccordingToSchemaConverterImpl, _super);
    function AdministrationAccordingToSchemaConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    AdministrationAccordingToSchemaConverterImpl.prototype.canConvert = function (dosage) {
        return dosage.isAdministrationAccordingToSchema();
    };
    AdministrationAccordingToSchemaConverterImpl.prototype.doConvert = function (dosage) {
        return "Dosering efter skriftlig anvisning";
    };
    return AdministrationAccordingToSchemaConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 41 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return FreeTextConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var FreeTextConverterImpl = (function (_super) {
    __extends(FreeTextConverterImpl, _super);
    function FreeTextConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    FreeTextConverterImpl.prototype.canConvert = function (dosage) {
        return dosage.freeText !== undefined;
    };
    FreeTextConverterImpl.prototype.doConvert = function (dosage) {
        return dosage.freeText.getText();
    };
    return FreeTextConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 42 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MorningNoonEveningNightAndAccordingToNeedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__SimpleLimitedAccordingToNeedConverterImpl__ = __webpack_require__(18);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var MorningNoonEveningNightAndAccordingToNeedConverterImpl = (function (_super) {
    __extends(MorningNoonEveningNightAndAccordingToNeedConverterImpl, _super);
    function MorningNoonEveningNightAndAccordingToNeedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.getDayNumber() !== 1)
            return false;
        if (day.containsTimedDose())
            return false;
        if (day.containsPlainNotAccordingToNeedDose())
            return false;
        if (!day.containsMorningNoonEveningNightDoses())
            return false;
        if (!day.containsAccordingToNeedDose())
            return false;
        return true;
    };
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += MorningNoonEveningNightAndAccordingToNeedConverterImpl.getMorningText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightAndAccordingToNeedConverterImpl.getNoonText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightAndAccordingToNeedConverterImpl.getEveningText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightAndAccordingToNeedConverterImpl.getNightText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightAndAccordingToNeedConverterImpl.getSupplText(structure.getSupplText());
        text += ", samt " + new __WEBPACK_IMPORTED_MODULE_1__SimpleLimitedAccordingToNeedConverterImpl__["a" /* SimpleLimitedAccordingToNeedConverterImpl */]().doConvert(dosage);
        return text.toString();
    };
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.getMorningText = function (day, unitOrUnits) {
        var text = "";
        if (day.getMorningDose()) {
            text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getMorningDose(), unitOrUnits);
            if (day.getMorningDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.getNoonText = function (day, unitOrUnits) {
        var text = "";
        if (day.getNoonDose()) {
            if (day.getMorningDose() && (day.getEveningDose() || day.getNightDose()))
                text += ", ";
            else if (day.getMorningDose())
                text += " og ";
            if (!day.allDosesHaveTheSameQuantity())
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNoonDose(), unitOrUnits);
            else if (day.getMorningDose())
                text += day.getNoonDose().getLabel();
            else
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNoonDose(), unitOrUnits);
            if (day.getNoonDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.getEveningText = function (day, unitOrUnits) {
        var text = "";
        if (day.getEveningDose()) {
            if ((day.getMorningDose() || day.getNoonDose()) && day.getNightDose())
                text += ", ";
            else if (day.getMorningDose() || day.getNoonDose())
                text += " og ";
            if (!day.allDosesHaveTheSameQuantity())
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getEveningDose(), unitOrUnits);
            else if (day.getMorningDose() || day.getNoonDose())
                text += day.getEveningDose().getLabel();
            else
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getEveningDose(), unitOrUnits);
            if (day.getEveningDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.getNightText = function (day, unitOrUnits) {
        var text = "";
        if (day.getNightDose()) {
            if (day.getMorningDose() || day.getNoonDose() || day.getEveningDose())
                text += " og ";
            if (!day.allDosesHaveTheSameQuantity())
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNightDose(), unitOrUnits);
            else if (day.getMorningDose() || day.getNoonDose() || day.getEveningDose())
                text += day.getNightDose().getLabel();
            else
                text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getNightDose(), unitOrUnits);
            if (day.getNightDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightAndAccordingToNeedConverterImpl.getSupplText = function (supplText) {
        var text = "";
        if (supplText)
            text += __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].maybeAddSpace(supplText) + supplText;
        return text;
    };
    return MorningNoonEveningNightAndAccordingToNeedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 43 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MorningNoonEveningNightEyeOrEarConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var MorningNoonEveningNightEyeOrEarConverterImpl = (function (_super) {
    __extends(MorningNoonEveningNightEyeOrEarConverterImpl, _super);
    function MorningNoonEveningNightEyeOrEarConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MorningNoonEveningNightEyeOrEarConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.getDayNumber() !== 1)
            return false;
        if (day.containsPlainDose() || day.containsTimedDose())
            return false;
        if (day.containsAccordingToNeedDose())
            return false;
        if (!day.allDosesHaveTheSameQuantity())
            return false;
        if (day.getAllDoses()[0].getDoseQuantity() === undefined)
            return false;
        if (!__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].hasIntegerValue(day.getAllDoses()[0].getDoseQuantity()))
            return false;
        var quantity = day.getAllDoses()[0].getDoseQuantity();
        if (!(quantity % 2 === 0))
            return false;
        if (structure.getSupplText() === undefined)
            return false;
        if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øje")) {
            if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strStartsWith(structure.getSupplText(), ",")) {
                if (structure.getSupplText() !== ", " + (quantity / 2) + " i hvert øje") {
                    return false;
                }
            }
            else {
                if (structure.getSupplText() !== (quantity / 2) + " i hvert øje") {
                    return false;
                }
            }
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øre")) {
            if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strStartsWith(structure.getSupplText(), ",")) {
                if (structure.getSupplText() !== ", " + (quantity / 2) + " i hvert øre") {
                    return false;
                }
            }
            else {
                if (structure.getSupplText() !== (quantity / 2) + " i hvert øre") {
                    return false;
                }
            }
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert næsebor")) {
            if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strStartsWith(structure.getSupplText(), ",")) {
                if (structure.getSupplText() !== ", " + (quantity / 2) + " i hvert næsebor") {
                    return false;
                }
            }
            else {
                if (structure.getSupplText() !== (quantity / 2) + " i hvert næsebor") {
                    return false;
                }
            }
        }
        else {
            return false;
        }
        return true;
    };
    MorningNoonEveningNightEyeOrEarConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += MorningNoonEveningNightEyeOrEarConverterImpl.getMorningText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightEyeOrEarConverterImpl.getNoonText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightEyeOrEarConverterImpl.getEveningText(day, dosage.structures.getUnitOrUnits());
        text += MorningNoonEveningNightEyeOrEarConverterImpl.getNightText(day, dosage.structures.getUnitOrUnits());
        var supplText = "";
        if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øje")) {
            supplText = " i begge øjne";
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øre")) {
            supplText = " i begge ører";
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert næsebor")) {
            supplText = " i begge næsebor";
        }
        text += supplText;
        return text;
    };
    MorningNoonEveningNightEyeOrEarConverterImpl.getMorningText = function (day, unitOrUnits) {
        var text = "";
        if (day.getMorningDose()) {
            text += this.toDoseLabelUnitValue(day.getMorningDose().getDoseQuantity() / 2, day.getMorningDose().getLabel(), unitOrUnits);
            if (day.getMorningDose().getIsAccordingToNeed()) {
                text += " efter behov";
            }
        }
        return text;
    };
    MorningNoonEveningNightEyeOrEarConverterImpl.getNoonText = function (day, unitOrUnits) {
        var text = "";
        if (day.getNoonDose()) {
            if (day.getMorningDose() && (day.getEveningDose() || day.getNightDose()))
                text += ", ";
            else if (day.getMorningDose())
                text += " og ";
            if (day.getMorningDose())
                text += day.getNoonDose().getLabel();
            else
                text += this.toDoseLabelUnitValue(day.getNoonDose().getDoseQuantity() / 2, day.getNoonDose().getLabel(), unitOrUnits);
            if (day.getNoonDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightEyeOrEarConverterImpl.getEveningText = function (day, unitOrUnits) {
        var text = "";
        if (day.getEveningDose()) {
            if ((day.getMorningDose() || day.getNoonDose()) && day.getNightDose())
                text += ", ";
            else if (day.getMorningDose() || day.getNoonDose())
                text += " og ";
            if (day.getMorningDose() || day.getNoonDose())
                text += day.getEveningDose().getLabel();
            else
                text += this.toDoseLabelUnitValue(day.getEveningDose().getDoseQuantity() / 2, day.getEveningDose().getLabel(), unitOrUnits);
            if (day.getEveningDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    MorningNoonEveningNightEyeOrEarConverterImpl.getNightText = function (day, unitOrUnits) {
        var text = "";
        if (day.getNightDose()) {
            if (day.getMorningDose() || day.getNoonDose() || day.getEveningDose())
                text += " og ";
            if (day.getMorningDose() || day.getNoonDose() || day.getEveningDose())
                text += day.getNightDose().getLabel();
            else
                text += this.toDoseLabelUnitValue(day.getNightDose().getDoseQuantity() / 2, day.getNightDose().getLabel(), unitOrUnits);
            if (day.getNightDose().getIsAccordingToNeed())
                text += " efter behov";
        }
        return text;
    };
    return MorningNoonEveningNightEyeOrEarConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 44 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return WeeklyMorningNoonEveningNightConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__ = __webpack_require__(9);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__longtextconverterimpl_WeeklyRepeatedConverterImpl__ = __webpack_require__(11);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var WeeklyMorningNoonEveningNightConverterImpl = (function (_super) {
    __extends(WeeklyMorningNoonEveningNightConverterImpl, _super);
    function WeeklyMorningNoonEveningNightConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    WeeklyMorningNoonEveningNightConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 7)
            return false;
        if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
            return false;
        if (structure.getDays().length > 7)
            return false;
        if (structure.getDays()[0].getDayNumber() === 0)
            return false;
        if (structure.getDays()[structure.getDays().length - 1].getDayNumber() > 7)
            return false;
        if (structure.containsAccordingToNeedDose() || structure.containsPlainDose() || structure.containsTimedDose())
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        return true;
    };
    WeeklyMorningNoonEveningNightConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var daysOfWeek = __WEBPACK_IMPORTED_MODULE_2__longtextconverterimpl_WeeklyRepeatedConverterImpl__["a" /* WeeklyRepeatedConverterImpl */].sortDaysOfWeek(structure);
        var text = "";
        var firstDay = daysOfWeek[0];
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getMorningText(firstDay.getDay(), dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getNoonText(firstDay.getDay(), dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getEveningText(firstDay.getDay(), dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getNightText(firstDay.getDay(), dosage.structures.getUnitOrUnits());
        var i = 0;
        for (var _i = 0, daysOfWeek_1 = daysOfWeek; _i < daysOfWeek_1.length; _i++) {
            var d = daysOfWeek_1[_i];
            if (i === daysOfWeek.length - 1 && daysOfWeek.length > 1)
                text += " og " + d.getName().toLowerCase();
            else if (i === 0)
                text += " " + d.getName().toLowerCase();
            else if (i > 0)
                text += ", " + d.getName().toLowerCase();
            i++;
        }
        text += " hver uge";
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getSupplText(structure.getSupplText());
        return text;
    };
    return WeeklyMorningNoonEveningNightConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 45 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return WeeklyRepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__longtextconverterimpl_WeeklyRepeatedConverterImpl__ = __webpack_require__(11);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var WeeklyRepeatedConverterImpl = (function (_super) {
    __extends(WeeklyRepeatedConverterImpl, _super);
    function WeeklyRepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    WeeklyRepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 7)
            return false;
        if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
            return false;
        if (structure.getDays().length > 7 || structure.getDays().length === 0)
            return false;
        if (structure.getDays()[0].getDayNumber() === 0)
            return false;
        if (structure.getDays()[structure.getDays().length - 1].getDayNumber() > 7)
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        if (!structure.allDosesAreTheSame())
            return false;
        if (structure.containsAccordingToNeedDose() || structure.containsMorningNoonEveningNightDoses() || structure.containsTimedDose())
            return false;
        return true;
    };
    WeeklyRepeatedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        // Append dosage
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        // Add times daily
        if (day.getNumberOfDoses() > 1)
            text += " " + day.getNumberOfDoses() + " gange daglig";
        // Add days
        text += WeeklyRepeatedConverterImpl.makeDays(structure);
        text += " hver uge";
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_2__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text.toString();
    };
    WeeklyRepeatedConverterImpl.makeDays = function (structure) {
        var text = "";
        // Add days
        var daysOfWeek = __WEBPACK_IMPORTED_MODULE_1__longtextconverterimpl_WeeklyRepeatedConverterImpl__["a" /* WeeklyRepeatedConverterImpl */].sortDaysOfWeek(structure);
        var i = 0;
        for (var _i = 0, daysOfWeek_1 = daysOfWeek; _i < daysOfWeek_1.length; _i++) {
            var d = daysOfWeek_1[_i];
            if (i === daysOfWeek.length - 1 && daysOfWeek.length > 1)
                text += " og " + d.getName().toLowerCase();
            else if (i === 0)
                text += " " + d.getName().toLowerCase();
            else if (i > 0)
                text += ", " + d.getName().toLowerCase();
            i++;
        }
        return text.toString();
    };
    return WeeklyRepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 46 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AdministrationAccordingToSchemaWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__ = __webpack_require__(6);

var AdministrationAccordingToSchemaWrapper = (function () {
    function AdministrationAccordingToSchemaWrapper(startDateOrDateTime, endDateOrDateTime) {
        this.startDateOrDateTime = startDateOrDateTime;
        this.endDateOrDateTime = endDateOrDateTime;
    }
    AdministrationAccordingToSchemaWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new AdministrationAccordingToSchemaWrapper(__WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */].fromJsonObject(jsonObject.startDateOrDateTime), __WEBPACK_IMPORTED_MODULE_0__DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */].fromJsonObject(jsonObject.endDateOrDateTime))
            : undefined;
    };
    AdministrationAccordingToSchemaWrapper.makeAdministrationAccordingToSchema = function (startDateOrDateTime, endDateOrDateTime) {
        return new AdministrationAccordingToSchemaWrapper(startDateOrDateTime, endDateOrDateTime);
    };
    AdministrationAccordingToSchemaWrapper.prototype.getStartDateOrDateTime = function () {
        return this.startDateOrDateTime;
    };
    AdministrationAccordingToSchemaWrapper.prototype.getEndDateOrDateTime = function () {
        return this.endDateOrDateTime;
    };
    return AdministrationAccordingToSchemaWrapper;
}());



/***/ }),
/* 47 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DayOfWeek; });
var DayOfWeek = (function () {
    function DayOfWeek(dayOfWeek, name, day) {
        this._dayOfWeek = dayOfWeek;
        this._name = name;
        this._day = day;
    }
    DayOfWeek.prototype.getDayOfWeek = function () {
        return this._dayOfWeek;
    };
    DayOfWeek.prototype.getName = function () {
        return this._name;
    };
    DayOfWeek.prototype.getDay = function () {
        return this._day;
    };
    return DayOfWeek;
}());



/***/ }),
/* 48 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TimedDoseWrapper; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LocalTime__ = __webpack_require__(31);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__DoseWrapper__ = __webpack_require__(4);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var TimedDoseWrapper = (function (_super) {
    __extends(TimedDoseWrapper, _super);
    function TimedDoseWrapper(time, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, doseQuantitystring, minimalDoseQuantitystring, maximalDoseQuantitystring, isAccordingToNeed) {
        var _this = _super.call(this, doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) || this;
        _this.time = time;
        return _this;
    }
    TimedDoseWrapper.fromJsonObject = function (jsonObject) {
        return jsonObject ?
            new TimedDoseWrapper(__WEBPACK_IMPORTED_MODULE_0__LocalTime__["a" /* LocalTime */].fromJsonObject(jsonObject.time), jsonObject.doseQuantity, jsonObject.minimalDoseQuantity, jsonObject.maximalDoseQuantity, jsonObject.doseQuantityString, jsonObject.minimalDoseQuantityString, jsonObject.maximalDoseQuantityString, jsonObject.isAccordingToNeed)
            : undefined;
    };
    TimedDoseWrapper.fromJsonObjectTime = function (jsonObject) {
        return new Date();
    };
    TimedDoseWrapper.prototype.getLabel = function () {
        return TimedDoseWrapper.LABEL + " " + this.time.toString();
    };
    TimedDoseWrapper.prototype.getTime = function () {
        return this.time.toString();
    };
    TimedDoseWrapper.prototype.theSameAs = function (other) {
        if (!(other instanceof TimedDoseWrapper))
            return false;
        if (!_super.prototype.theSameAs.call(this, other))
            return false;
        return this.getTime() === other.getTime();
    };
    TimedDoseWrapper.LABEL = "kl.";
    return TimedDoseWrapper;
}(__WEBPACK_IMPORTED_MODULE_1__DoseWrapper__["a" /* DoseWrapper */]));



/***/ }),
/* 49 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AbstractXMLGenerator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__MMANMapping__ = __webpack_require__(58);

var AbstractXMLGenerator = (function () {
    function AbstractXMLGenerator() {
        this.daysMappingRegExp = /dag\s(\d+):\s(\d+(\.\d+)?(;\d+(\.\d+)?)?)/g; /* Match på ex. "dag 1: 1.0", "dag 1: 1.2;2.3", "dag 1: 1 dag 2: 2" og "dag 1: 1;1 dag 2: 1;3" */
    }
    AbstractXMLGenerator.parseMapping = function (mapping) {
        var splittedMapping = mapping.split("+");
        var mmanMapping = new __WEBPACK_IMPORTED_MODULE_0__MMANMapping__["a" /* MMANMapping */]();
        if (splittedMapping.length > 0) {
            mmanMapping.setMorning(parseFloat(splittedMapping[0]));
        }
        if (splittedMapping.length > 1) {
            mmanMapping.setNoon(parseFloat(splittedMapping[1]));
        }
        if (splittedMapping.length > 2) {
            mmanMapping.setEvening(parseFloat(splittedMapping[2]));
        }
        if (splittedMapping.length > 3) {
            mmanMapping.setNight(parseFloat(splittedMapping[3]));
        }
        return mmanMapping;
    };
    AbstractXMLGenerator.prototype.escape = function (s) {
        if (s) {
            return s.replace("<", "&lt;").replace(">", "&gt;").replace("&", "&amp;").replace("\"", "&quot").replace("'", "&apos;");
        }
        return s;
    };
    return AbstractXMLGenerator;
}());



/***/ }),
/* 50 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosageType; });
var DosageType;
(function (DosageType) {
    DosageType[DosageType["AccordingToNeed"] = 0] = "AccordingToNeed";
    DosageType[DosageType["Temporary"] = 1] = "Temporary";
    DosageType[DosageType["Fixed"] = 2] = "Fixed";
    DosageType[DosageType["OneTime"] = 3] = "OneTime";
    DosageType[DosageType["Combined"] = 4] = "Combined";
    DosageType[DosageType["Unspecified"] = 5] = "Unspecified"; // ”ikke angivet”: Anvendes for doseringer oprettet gennem versioner før FMK 1.3 / 1.4, og hvor det ikke er muligt at bestemme typen, dvs. at doseringen er som fritekst eller som ”efter skema i lokalt system”. Der kan ikke oprettes doseringer med typen ”ikke angivet” via FMK 1.3 / 1.4.
})(DosageType || (DosageType = {}));


/***/ }),
/* 51 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CombinedTextConverter; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__LongTextConverter__ = __webpack_require__(10);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__ = __webpack_require__(8);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__DailyDosisCalculator__ = __webpack_require__(26);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__vowrapper_StructuresWrapper__ = __webpack_require__(7);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__CombinedConversion__ = __webpack_require__(56);






var CombinedTextConverter = (function () {
    function CombinedTextConverter() {
    }
    CombinedTextConverter.convertStr = function (jsonStr) {
        if (jsonStr === undefined || jsonStr === null) {
            return null;
        }
        return CombinedTextConverter.convert(JSON.parse(jsonStr));
    };
    CombinedTextConverter.convert = function (dosageJson) {
        if (dosageJson === undefined || dosageJson === null) {
            return null;
        }
        var dosage = __WEBPACK_IMPORTED_MODULE_3__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson);
        return CombinedTextConverter.convertWrapper(dosage);
    };
    CombinedTextConverter.convertWrapper = function (dosage) {
        var shortText = __WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__["a" /* ShortTextConverter */].getInstance().convertWrapper(dosage);
        var longText = __WEBPACK_IMPORTED_MODULE_0__LongTextConverter__["a" /* LongTextConverter */].getInstance().convertWrapper(dosage);
        var dailyDosis = __WEBPACK_IMPORTED_MODULE_2__DailyDosisCalculator__["a" /* DailyDosisCalculator */].calculateWrapper(dosage);
        var periodTexts = new Array();
        if (dosage.isStructured()) {
            for (var _i = 0, _a = dosage.structures.getStructures(); _i < _a.length; _i++) {
                var period = _a[_i];
                var structuresWithOnePeriod = new __WEBPACK_IMPORTED_MODULE_4__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](dosage.structures.getUnitOrUnits(), [period]);
                var dosageWrapperWithOnePeriod = __WEBPACK_IMPORTED_MODULE_3__vowrapper_DosageWrapper__["a" /* DosageWrapper */].makeStructuredDosage(structuresWithOnePeriod);
                var periodShortText = __WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__["a" /* ShortTextConverter */].getInstance().convertWrapper(dosageWrapperWithOnePeriod);
                var periodLongText = __WEBPACK_IMPORTED_MODULE_0__LongTextConverter__["a" /* LongTextConverter */].getInstance().convertWrapper(dosageWrapperWithOnePeriod);
                var dailyDosis_1 = __WEBPACK_IMPORTED_MODULE_2__DailyDosisCalculator__["a" /* DailyDosisCalculator */].calculateWrapper(dosageWrapperWithOnePeriod);
                periodTexts.push([periodShortText, periodLongText, dailyDosis_1]);
            }
        }
        return new __WEBPACK_IMPORTED_MODULE_5__CombinedConversion__["a" /* CombinedConversion */](shortText, longText, dailyDosis, periodTexts);
    };
    return CombinedTextConverter;
}());



/***/ }),
/* 52 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosageProposalXMLGenerator; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DosageProposalXML__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__XML140Generator__ = __webpack_require__(14);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__XML142Generator__ = __webpack_require__(15);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__XML144Generator__ = __webpack_require__(16);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__XML146Generator__ = __webpack_require__(29);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__AbstractXMLGenerator__ = __webpack_require__(49);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__ShortTextConverter__ = __webpack_require__(8);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__LongTextConverter__ = __webpack_require__(10);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__vowrapper_StructuresWrapper__ = __webpack_require__(7);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__vowrapper_StructureWrapper__ = __webpack_require__(13);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__vowrapper_UnitOrUnitsWrapper__ = __webpack_require__(25);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__vowrapper_DateOrDateTimeWrapper__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__vowrapper_DayWrapper__ = __webpack_require__(12);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__vowrapper_MorningDoseWrapper__ = __webpack_require__(21);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_15__vowrapper_NoonDoseWrapper__ = __webpack_require__(23);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_16__vowrapper_EveningDoseWrapper__ = __webpack_require__(19);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_17__vowrapper_NightDoseWrapper__ = __webpack_require__(22);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_18__vowrapper_PlainDoseWrapper__ = __webpack_require__(24);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_19__DosagePeriod__ = __webpack_require__(27);




















var DosageProposalXMLGenerator = (function () {
    function DosageProposalXMLGenerator() {
    }
    DosageProposalXMLGenerator.getPeriodStrings = function (s) {
        var firstBracePos = s.indexOf("{");
        if (firstBracePos === -1) {
            // Only one period without braces
            return [s];
        }
        var periods = [];
        var openBracePos = 0;
        var closeBracePos = 0;
        while (closeBracePos < s.length - 1) {
            openBracePos = s.indexOf("{", closeBracePos);
            closeBracePos = s.indexOf("}", openBracePos);
            if (closeBracePos === -1) {
                throw new Error("Mismatching {} braces in period string " + s);
            }
            periods.push(s.substr(openBracePos + 1, closeBracePos - openBracePos - 1));
        }
        return periods;
    };
    DosageProposalXMLGenerator.generateXMLSnippet = function (type, iteration, mapping, unitTextSingular, unitTextPlural, supplementaryText, beginDates, endDates, fmkversion, dosageProposalVersion) {
        if (dosageProposalVersion !== DosageProposalXMLGenerator.dosageProposalXMLGeneratorVersion) {
            throw new Error("Unsupported dosageProposalXMLGeneratorVersion, only version " + DosageProposalXMLGenerator.dosageProposalXMLGeneratorVersion + " is supported");
        }
        var periodTypes = DosageProposalXMLGenerator.getPeriodStrings(type);
        var periodMappings = DosageProposalXMLGenerator.getPeriodStrings(mapping);
        var periodIterations = DosageProposalXMLGenerator.getPeriodStrings(iteration).map(function (i) { return parseInt(i); });
        if (periodTypes.length !== periodMappings.length) {
            throw new Error("Number of periods in 'type' argument " + periodTypes.length + " differs from number of periods in 'mapping' argument " + periodMappings.length);
        }
        if (periodTypes.length !== beginDates.length) {
            throw new Error("Number of periods in 'type' argument " + periodTypes.length + " differs from number of periods in 'beginDates' argument " + beginDates.length);
        }
        if (periodTypes.length !== endDates.length) {
            throw new Error("Number of periods in 'type' argument " + periodTypes.length + " differs from number of periods in 'endDates' argument " + endDates.length);
        }
        if (periodTypes.length !== periodIterations.length) {
            throw new Error("Number of periods in 'type' argument " + periodTypes.length + " differs from number of periods in 'iteration' argument " + periodIterations.length);
        }
        var periodWrappers = [];
        var dosagePeriods = [];
        for (var periodNo = 0; periodNo < periodTypes.length; periodNo++) {
            periodWrappers.push(new __WEBPACK_IMPORTED_MODULE_10__vowrapper_StructureWrapper__["a" /* StructureWrapper */](periodIterations[periodNo], supplementaryText, new __WEBPACK_IMPORTED_MODULE_12__vowrapper_DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */](beginDates[periodNo], undefined), new __WEBPACK_IMPORTED_MODULE_12__vowrapper_DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */](endDates[periodNo], undefined), DosageProposalXMLGenerator.getDayWrappers(periodTypes[periodNo], periodMappings[periodNo]), undefined));
            dosagePeriods.push(new __WEBPACK_IMPORTED_MODULE_19__DosagePeriod__["a" /* DosagePeriod */](periodTypes[periodNo], periodMappings[periodNo], periodIterations[periodNo], beginDates[periodNo], endDates[periodNo]));
        }
        var xml = DosageProposalXMLGenerator.getXMLGenerator(fmkversion).generateXml(dosagePeriods, unitTextSingular, unitTextPlural, supplementaryText);
        var dosageWrapper = new __WEBPACK_IMPORTED_MODULE_8__vowrapper_DosageWrapper__["a" /* DosageWrapper */](undefined, undefined, new __WEBPACK_IMPORTED_MODULE_9__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](new __WEBPACK_IMPORTED_MODULE_11__vowrapper_UnitOrUnitsWrapper__["a" /* UnitOrUnitsWrapper */](undefined, unitTextSingular, unitTextPlural), periodWrappers));
        return new __WEBPACK_IMPORTED_MODULE_0__DosageProposalXML__["a" /* DosageProposalXML */](xml, __WEBPACK_IMPORTED_MODULE_6__ShortTextConverter__["a" /* ShortTextConverter */].getInstance().convertWrapper(dosageWrapper), __WEBPACK_IMPORTED_MODULE_7__LongTextConverter__["a" /* LongTextConverter */].getInstance().convertWrapper(dosageWrapper));
    };
    DosageProposalXMLGenerator.getDayWrappers = function (type, mapping) {
        var dayWrappers;
        if (type === "M+M+A+N") {
            var doses = [];
            var mmanMapping = __WEBPACK_IMPORTED_MODULE_5__AbstractXMLGenerator__["a" /* AbstractXMLGenerator */].parseMapping(mapping);
            if (mmanMapping.getMorning()) {
                doses.push(new __WEBPACK_IMPORTED_MODULE_14__vowrapper_MorningDoseWrapper__["a" /* MorningDoseWrapper */](mmanMapping.getMorning(), undefined, undefined, undefined, undefined, undefined, false));
            }
            if (mmanMapping.getNoon()) {
                doses.push(new __WEBPACK_IMPORTED_MODULE_15__vowrapper_NoonDoseWrapper__["a" /* NoonDoseWrapper */](mmanMapping.getNoon(), undefined, undefined, undefined, undefined, undefined, false));
            }
            if (mmanMapping.getEvening()) {
                doses.push(new __WEBPACK_IMPORTED_MODULE_16__vowrapper_EveningDoseWrapper__["a" /* EveningDoseWrapper */](mmanMapping.getEvening(), undefined, undefined, undefined, undefined, undefined, false));
            }
            if (mmanMapping.getNight()) {
                doses.push(new __WEBPACK_IMPORTED_MODULE_17__vowrapper_NightDoseWrapper__["a" /* NightDoseWrapper */](mmanMapping.getNight(), undefined, undefined, undefined, undefined, undefined, false));
            }
            dayWrappers = [new __WEBPACK_IMPORTED_MODULE_13__vowrapper_DayWrapper__["a" /* DayWrapper */](1, doses)];
        }
        else {
            if (mapping.indexOf("dag ") >= 0) {
                var result = void 0;
                dayWrappers = [];
                while ((result = this.xml146Generator.daysMappingRegExp.exec(mapping)) != null) {
                    var dayno = parseInt(result[1]);
                    var day = new __WEBPACK_IMPORTED_MODULE_13__vowrapper_DayWrapper__["a" /* DayWrapper */](dayno, DosageProposalXMLGenerator.getDoses(result[2], type));
                    dayWrappers.push(day);
                }
            }
            else {
                var day = new __WEBPACK_IMPORTED_MODULE_13__vowrapper_DayWrapper__["a" /* DayWrapper */](1, DosageProposalXMLGenerator.getDoses(mapping, type));
                dayWrappers = [day];
            }
        }
        return dayWrappers;
    };
    DosageProposalXMLGenerator.getDoses = function (quantity, type) {
        var splittedQuantity = quantity.split(";");
        var doses = [];
        for (var _i = 0, splittedQuantity_1 = splittedQuantity; _i < splittedQuantity_1.length; _i++) {
            var quantity_1 = splittedQuantity_1[_i];
            doses.push(new __WEBPACK_IMPORTED_MODULE_18__vowrapper_PlainDoseWrapper__["a" /* PlainDoseWrapper */](parseFloat(quantity_1), undefined, undefined, undefined, undefined, undefined, type === "PN"));
        }
        return doses;
    };
    DosageProposalXMLGenerator.getXMLGenerator = function (fmkversion) {
        switch (fmkversion) {
            case "FMK140":
                return DosageProposalXMLGenerator.xml140Generator;
            case "FMK142":
                return DosageProposalXMLGenerator.xml142Generator;
            case "FMK144":
                return DosageProposalXMLGenerator.xml144Generator;
            case "FMK146":
                return DosageProposalXMLGenerator.xml146Generator;
            default:
                throw new Error("Unexpected fmk version: " + fmkversion);
        }
    };
    DosageProposalXMLGenerator.xml140Generator = new __WEBPACK_IMPORTED_MODULE_1__XML140Generator__["a" /* XML140Generator */]();
    DosageProposalXMLGenerator.xml142Generator = new __WEBPACK_IMPORTED_MODULE_2__XML142Generator__["a" /* XML142Generator */]();
    DosageProposalXMLGenerator.xml144Generator = new __WEBPACK_IMPORTED_MODULE_3__XML144Generator__["a" /* XML144Generator */]();
    DosageProposalXMLGenerator.xml146Generator = new __WEBPACK_IMPORTED_MODULE_4__XML146Generator__["a" /* XML146Generator */]();
    DosageProposalXMLGenerator.dosageProposalXMLGeneratorVersion = 1;
    return DosageProposalXMLGenerator;
}());



/***/ }),
/* 53 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DosageTypeCalculator144; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__DosageTypeCalculator__ = __webpack_require__(30);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__DosageType__ = __webpack_require__(50);



/***
 * From FMK 1.4.4 and above, only 3 dosage types are available: Fixed, AccordingToNeed and Combined (besides Unspec.).
 * Use this DosageTypeCalculator144 for all services using FMK 1.4.4 and higher, and use DosageTypeCalculator for FMK 1.4.2 and below
 *
 *
 */
var DosageTypeCalculator144 = (function () {
    function DosageTypeCalculator144() {
    }
    DosageTypeCalculator144.calculate = function (dosageJson) {
        return DosageTypeCalculator144.calculateWrapper(__WEBPACK_IMPORTED_MODULE_1__vowrapper_DosageWrapper__["a" /* DosageWrapper */].fromJsonObject(dosageJson));
    };
    DosageTypeCalculator144.calculateStr = function (jsonStr) {
        return DosageTypeCalculator144.calculate(JSON.parse(jsonStr));
    };
    DosageTypeCalculator144.calculateWrapper = function (dosage) {
        if (dosage.isAdministrationAccordingToSchema())
            return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Unspecified;
        else if (dosage.isFreeText())
            return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Unspecified;
        else
            return DosageTypeCalculator144.calculateFromStructures(dosage.structures);
    };
    DosageTypeCalculator144.calculateFromStructures = function (structures) {
        if (DosageTypeCalculator144.hasAtLeastOneCombinedStructure(structures) || DosageTypeCalculator144.hasMixedNotEmptyStructures(structures)) {
            return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Combined;
        }
        else {
            /* Invariant at this point: all structures are a) fixed or empty .... or b) PN or empty
             * The dosagetype is then found by finding the dosagetype of the first not-empty Structure
             * If only empty structures are present, return fixed...just because */
            var fixedStructures = [];
            var pnStructures = [];
            DosageTypeCalculator144.splitInFixedAndPN(structures, fixedStructures, pnStructures);
            if (fixedStructures.length === 0) {
                return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].AccordingToNeed;
            }
            else {
                return pnStructures.length === 0 ? __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Fixed : __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Combined;
            }
        }
    };
    DosageTypeCalculator144.structureSorter = function (s1, s2) {
        return s1.getStartDateOrDateTime().getDateOrDateTime().getTime() - s2.getStartDateOrDateTime().getDateOrDateTime().getTime();
    };
    /*
     * Precondition: all structures contains only fixed or pn doses
     * In case some contained mixed, this method would never have been called, since we then know that the DosageType would be combined
     */
    DosageTypeCalculator144.splitInFixedAndPN = function (structures, fixedStructures, pnStructures) {
        var emptyStructures = [];
        structures.getStructures().forEach(function (s) {
            if (s.isEmpty()) {
                emptyStructures.push(s);
            }
            else {
                if (s.containsAccordingToNeedDosesOnly()) {
                    pnStructures.push(s);
                }
                else {
                    fixedStructures.push(s);
                }
            }
        });
        fixedStructures.sort(DosageTypeCalculator144.structureSorter);
        pnStructures.sort(DosageTypeCalculator144.structureSorter);
        emptyStructures.sort(DosageTypeCalculator144.structureSorter);
        /* Find all gaps in the fixed and pn structures, and insert fitting emptystructures in the gaps
         * We know that some should fit, since it is validated in the DosageStructureValidator, that no gaps are present.
         */
        DosageTypeCalculator144.fillGapsWithEmptyPeriods(fixedStructures, emptyStructures);
        DosageTypeCalculator144.fillGapsWithEmptyPeriods(pnStructures, emptyStructures);
        /* in case any emptystructures are left, they should be placed either at the beginning or end of either the fixed or the pn structures */
        var unhandledEmptyStructures = [];
        for (var i = 0; i < emptyStructures.length; i++) {
            var es = emptyStructures[i];
            var handled = false;
            if (fixedStructures.length > 0) {
                if (DosageTypeCalculator144.abuts(es, fixedStructures[0])) {
                    fixedStructures.splice(0, 0, es);
                    handled = true;
                }
                else if (DosageTypeCalculator144.abuts(fixedStructures[fixedStructures.length - 1], es)) {
                    fixedStructures.push(es);
                    handled = true;
                }
            }
            if (!handled && pnStructures.length > 0) {
                if (DosageTypeCalculator144.abuts(es, pnStructures[0])) {
                    pnStructures.splice(0, 0, es);
                    handled = true;
                }
                else if (DosageTypeCalculator144.abuts(pnStructures[pnStructures.length - 1], es)) {
                    pnStructures.push(es);
                    handled = true;
                }
            }
            if (!handled) {
                unhandledEmptyStructures.push(es);
            }
        }
        /* In case there are still unhandled empy structures, and either fixed or pn-structures are completely empty, they should go there */
        var noFixedStructures = fixedStructures.length === 0;
        var noPNStructures = pnStructures.length === 0;
        unhandledEmptyStructures.forEach(function (es) {
            if (noFixedStructures) {
                fixedStructures.push(es);
            }
            else if (noPNStructures) {
                pnStructures.push(es);
            }
        });
    };
    /* Check if second wrapper comes just after the first, without gaps or overlaps */
    DosageTypeCalculator144.abuts = function (first, second) {
        if (first.getEndDateOrDateTime() != null) {
            if (first.getEndDateOrDateTime().getDate() != null
                && second.getStartDateOrDateTime().getDate() != null
                && DosageTypeCalculator144.dateAbuts(first.getEndDateOrDateTime().getDate(), second.getStartDateOrDateTime().getDate())) {
                return true;
            }
            else if (first.getEndDateOrDateTime().getDateTime() != null
                && second.getStartDateOrDateTime().getDateTime() != null
                && DosageTypeCalculator144.dateTimeAbuts(first.getEndDateOrDateTime().getDateTime(), second.getStartDateOrDateTime().getDateTime())) {
                return true;
            }
            // If an interval ends with a date and the next ends with a datetime we cannot determine if they abut
        }
        // No end date, definitely not abut
        return false;
    };
    DosageTypeCalculator144.treatAsUTC = function (date) {
        var result = new Date(date.valueOf());
        result.setMinutes(result.getMinutes() - result.getTimezoneOffset());
        return result;
    };
    DosageTypeCalculator144.daysBetween = function (startDate, endDate) {
        var millisecondsPerDay = 24 * 60 * 60 * 1000;
        return (DosageTypeCalculator144.treatAsUTC(endDate).valueOf() - DosageTypeCalculator144.treatAsUTC(startDate).valueOf()) / millisecondsPerDay;
    };
    DosageTypeCalculator144.secondsBetween = function (startDate, endDate) {
        var millisecondsSecond = 1000;
        return (DosageTypeCalculator144.treatAsUTC(endDate).valueOf() - DosageTypeCalculator144.treatAsUTC(startDate).valueOf()) / millisecondsSecond;
    };
    DosageTypeCalculator144.dateTimeAbuts = function (dateTime1, dateTime2) {
        var secondsBetween = DosageTypeCalculator144.secondsBetween(dateTime1, dateTime2);
        return secondsBetween >= 0 && secondsBetween <= 1;
    };
    DosageTypeCalculator144.dateAbuts = function (d1, d2) {
        return DosageTypeCalculator144.daysBetween(d1, d2) === 1;
    };
    DosageTypeCalculator144.fillGapsWithEmptyPeriods = function (structures, emptyStructures) {
        var structuresSize = structures.length;
        for (var i = 0; i < structuresSize - 1; i++) {
            if (DosageTypeCalculator144.hasGap(structures[i], structures[i + 1])) {
                var indexOfemptyStructureFittingGap = DosageTypeCalculator144.findIndexOfEmptyStructuresThatFitsGap(emptyStructures, structures[i], structures[i + 1]);
                if (indexOfemptyStructureFittingGap > -1) {
                    var emptyStructureFittingGap = emptyStructures.splice(indexOfemptyStructureFittingGap, 1)[0];
                    structures.splice(i + 1, 0, emptyStructureFittingGap);
                    structuresSize++;
                }
            }
        }
    };
    DosageTypeCalculator144.findIndexOfEmptyStructuresThatFitsGap = function (emptyStructures, structureWrapper1, structureWrapper2) {
        for (var i = 0; i < emptyStructures.length; i++) {
            if (DosageTypeCalculator144.abuts(structureWrapper1, emptyStructures[i]) && DosageTypeCalculator144.abuts(emptyStructures[i], structureWrapper2)) {
                return i;
            }
        }
        return -1;
    };
    DosageTypeCalculator144.hasGap = function (structureWrapper1, structureWrapper2) {
        return !DosageTypeCalculator144.abuts(structureWrapper1, structureWrapper2);
    };
    DosageTypeCalculator144.firstNotEmptyStructure = function (structures) {
        return structures.getStructures().filter(function (s) { return s.getDays().length > 0; })[0];
    };
    /* Check if at least one not-empty Structure containing both fixed and PN Dose's exists */
    DosageTypeCalculator144.hasAtLeastOneCombinedStructure = function (structures) {
        return structures.getStructures().some(function (structure, index, array) { return structure.containsAccordingToNeedDose() && !structure.containsAccordingToNeedDosesOnly(); });
    };
    /* Check if at least one structure with fixed dose's only AND at least one structure with PN only doses exists
     * Precondition: since this method is called after hasAtLeastOneCombinedStructure(),
     * then we suggest that all Structures contains either fixed or PN doses, not combined inside one Structure
     */
    DosageTypeCalculator144.hasMixedNotEmptyStructures = function (structures) {
        if (structures && structures.getStructures()) {
            var i = 0;
            var firstNotEmptyStructure = void 0;
            // Find first none-empty structure
            while (firstNotEmptyStructure === undefined && i < structures.getStructures().length) {
                var firstNotEmptyStructureCandidate = structures.getStructures()[i];
                if (firstNotEmptyStructureCandidate.getDays().length > 0) {
                    firstNotEmptyStructure = firstNotEmptyStructureCandidate;
                }
                else {
                    i++;
                }
            }
            if (firstNotEmptyStructure != null) {
                var firstType = DosageTypeCalculator144.calculateFromStructure(firstNotEmptyStructure);
                for (var j = i; j < structures.getStructures().length; j++) {
                    var structure = structures.getStructures()[j];
                    if (structure.getDays().length > 0 && firstType !== DosageTypeCalculator144.calculateFromStructure(structure)) {
                        return true;
                    }
                }
            }
        }
        return false;
    };
    DosageTypeCalculator144.calculateFromStructure = function (structure) {
        if (__WEBPACK_IMPORTED_MODULE_0__DosageTypeCalculator__["a" /* DosageTypeCalculator */].isAccordingToNeed(structure)) {
            return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].AccordingToNeed;
        }
        else if (structure.containsAccordingToNeedDose()) {
            return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Combined;
        }
        else {
            return __WEBPACK_IMPORTED_MODULE_2__DosageType__["a" /* DosageType */].Fixed;
        }
    };
    return DosageTypeCalculator144;
}());



/***/ }),
/* 54 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Factory; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__vowrapper_StructureWrapper__ = __webpack_require__(13);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__vowrapper_FreeTextWrapper__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__LongTextConverter__ = __webpack_require__(10);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__ShortTextConverter__ = __webpack_require__(8);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__vowrapper_DateOrDateTimeWrapper__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__vowrapper_DosageWrapper__ = __webpack_require__(2);






var Factory = (function () {
    function Factory() {
    }
    Factory.getLongTextConverter = function () {
        return __WEBPACK_IMPORTED_MODULE_2__LongTextConverter__["a" /* LongTextConverter */].getInstance();
    };
    Factory.getShortTextConverter = function () {
        return __WEBPACK_IMPORTED_MODULE_3__ShortTextConverter__["a" /* ShortTextConverter */].getInstance();
    };
    Factory.getCombinedTextConverter = function () {
        return null;
    };
    // These are dummy methods in order to let webpack know about the types
    Factory.getDateOrDateTimeWrapper = function () { return new __WEBPACK_IMPORTED_MODULE_4__vowrapper_DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */](null, null); };
    Factory.getStructureWrapper = function () { return new __WEBPACK_IMPORTED_MODULE_0__vowrapper_StructureWrapper__["a" /* StructureWrapper */](null, null, null, null, null, null); };
    Factory.getDosageWrapper = function () { return new __WEBPACK_IMPORTED_MODULE_5__vowrapper_DosageWrapper__["a" /* DosageWrapper */](null, new __WEBPACK_IMPORTED_MODULE_1__vowrapper_FreeTextWrapper__["a" /* FreeTextWrapper */](new __WEBPACK_IMPORTED_MODULE_4__vowrapper_DateOrDateTimeWrapper__["a" /* DateOrDateTimeWrapper */](new Date(), null), null, "dims"), null); };
    return Factory;
}());



/***/ }),
/* 55 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoggerService; });
var LoggerService = (function () {
    function LoggerService() {
    }
    LoggerService.info = function (msg) {
        console.log("info", msg);
    };
    LoggerService.error = function (msg) {
        console.log("error", msg);
    };
    LoggerService.debug = function (msg) {
        console.log("debug", msg);
    };
    return LoggerService;
}());



/***/ }),
/* 56 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CombinedConversion; });
var CombinedConversion = (function () {
    function CombinedConversion(combinedShortText, combinedLongText, combinedDailyDosis, periodTexts) {
        this.combinedShortText = combinedShortText;
        this.combinedLongText = combinedLongText;
        this.combinedDailyDosis = combinedDailyDosis;
        this.periodTexts = periodTexts;
    }
    CombinedConversion.prototype.getCombinedShortText = function () {
        return this.combinedShortText;
    };
    CombinedConversion.prototype.getCombinedLongText = function () {
        return this.combinedLongText;
    };
    CombinedConversion.prototype.getCombinedDailyDosis = function () {
        return this.combinedDailyDosis;
    };
    CombinedConversion.prototype.getPeriodTexts = function () {
        return this.periodTexts;
    };
    return CombinedConversion;
}());



/***/ }),
/* 57 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DailyDosis; });
var DailyDosis = (function () {
    function DailyDosis(value, interval, unitOrUnits) {
        this.value = value;
        this.interval = interval;
        this.unitOrUnits = unitOrUnits;
    }
    DailyDosis.prototype.getValue = function () {
        return this.value;
    };
    DailyDosis.prototype.getInterval = function () {
        return this.interval;
    };
    DailyDosis.prototype.getUnitOrUnits = function () {
        return this.unitOrUnits;
    };
    DailyDosis.prototype.isValue = function () {
        return this.value !== undefined && this.value != null;
    };
    DailyDosis.prototype.isInterval = function () {
        return this.interval !== undefined;
    };
    DailyDosis.prototype.isNone = function () {
        return this.value !== undefined && this.interval !== undefined;
    };
    return DailyDosis;
}());



/***/ }),
/* 58 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MMANMapping; });
var MMANMapping = (function () {
    function MMANMapping() {
    }
    MMANMapping.prototype.getMorning = function () {
        return this.morning;
    };
    MMANMapping.prototype.setMorning = function (morning) {
        this.morning = morning;
    };
    MMANMapping.prototype.getNoon = function () {
        return this.noon;
    };
    MMANMapping.prototype.setNoon = function (noon) {
        this.noon = noon;
    };
    MMANMapping.prototype.getEvening = function () {
        return this.evening;
    };
    MMANMapping.prototype.setEvening = function (evening) {
        this.evening = evening;
    };
    MMANMapping.prototype.getNight = function () {
        return this.night;
    };
    MMANMapping.prototype.setNight = function (night) {
        this.night = night;
    };
    return MMANMapping;
}());



/***/ }),
/* 59 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__longtextconverterimpl_AdministrationAccordingToSchemaConverterImpl__ = __webpack_require__(33);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "LongAdministrationAccordingToSchemaConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_0__longtextconverterimpl_AdministrationAccordingToSchemaConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__longtextconverterimpl_DailyRepeatedConverterImpl__ = __webpack_require__(34);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DailyRepeatedConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_1__longtextconverterimpl_DailyRepeatedConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__longtextconverterimpl_DefaultLongTextConverterImpl__ = __webpack_require__(35);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DefaultLongTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_2__longtextconverterimpl_DefaultLongTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__longtextconverterimpl_DefaultMultiPeriodeLongTextConverterImpl__ = __webpack_require__(36);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DefaultMultiPeriodeLongTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_3__longtextconverterimpl_DefaultMultiPeriodeLongTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__longtextconverterimpl_EmptyStructureConverterImpl__ = __webpack_require__(37);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "EmptyStructureConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_4__longtextconverterimpl_EmptyStructureConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__longtextconverterimpl_FreeTextConverterImpl__ = __webpack_require__(38);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "FreeTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_5__longtextconverterimpl_FreeTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__longtextconverterimpl_LongTextConverterImpl__ = __webpack_require__(3);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "LongTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_6__longtextconverterimpl_LongTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__longtextconverterimpl_SimpleLongTextConverterImpl__ = __webpack_require__(17);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "SimpleLongTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_7__longtextconverterimpl_SimpleLongTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__longtextconverterimpl_TwoDaysRepeatedConverterImpl__ = __webpack_require__(39);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "TwoDaysRepeatedConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_8__longtextconverterimpl_TwoDaysRepeatedConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__longtextconverterimpl_WeeklyRepeatedConverterImpl__ = __webpack_require__(11);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "LongWeeklyRepeatedConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_9__longtextconverterimpl_WeeklyRepeatedConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__shorttextconverterimpl_AdministrationAccordingToSchemaConverterImpl__ = __webpack_require__(40);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "ShortAdministrationAccordingToSchemaConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_10__shorttextconverterimpl_AdministrationAccordingToSchemaConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__shorttextconverterimpl_FreeTextConverterImpl__ = __webpack_require__(41);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "ShortFreeTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_11__shorttextconverterimpl_FreeTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__shorttextconverterimpl_MorningNoonEveningNightAndAccordingToNeedConverterImpl__ = __webpack_require__(42);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "MorningNoonEveningNightAndAccordingToNeedConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_12__shorttextconverterimpl_MorningNoonEveningNightAndAccordingToNeedConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__shorttextconverterimpl_MorningNoonEveningNightConverterImpl__ = __webpack_require__(9);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "MorningNoonEveningNightConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_13__shorttextconverterimpl_MorningNoonEveningNightConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__shorttextconverterimpl_MorningNoonEveningNightEyeOrEarConverterImpl__ = __webpack_require__(43);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "MorningNoonEveningNightEyeOrEarConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_14__shorttextconverterimpl_MorningNoonEveningNightEyeOrEarConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_15__shorttextconverterimpl_ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "ShortTextConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_15__shorttextconverterimpl_ShortTextConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_16__shorttextconverterimpl_SimpleLimitedAccordingToNeedConverterImpl__ = __webpack_require__(18);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "SimpleLimitedAccordingToNeedConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_16__shorttextconverterimpl_SimpleLimitedAccordingToNeedConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_17__shorttextconverterimpl_WeeklyMorningNoonEveningNightConverterImpl__ = __webpack_require__(44);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "WeeklyMorningNoonEveningNightConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_17__shorttextconverterimpl_WeeklyMorningNoonEveningNightConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_18__shorttextconverterimpl_WeeklyRepeatedConverterImpl__ = __webpack_require__(45);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "ShortWeeklyRepeatedConverterImpl", function() { return __WEBPACK_IMPORTED_MODULE_18__shorttextconverterimpl_WeeklyRepeatedConverterImpl__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_19__vowrapper_AdministrationAccordingToSchemaWrapper__ = __webpack_require__(46);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "AdministrationAccordingToSchemaWrapper", function() { return __WEBPACK_IMPORTED_MODULE_19__vowrapper_AdministrationAccordingToSchemaWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_20__vowrapper_DateOrDateTimeWrapper__ = __webpack_require__(6);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DateOrDateTimeWrapper", function() { return __WEBPACK_IMPORTED_MODULE_20__vowrapper_DateOrDateTimeWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_21__vowrapper_DayOfWeek__ = __webpack_require__(47);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DayOfWeek", function() { return __WEBPACK_IMPORTED_MODULE_21__vowrapper_DayOfWeek__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_22__vowrapper_DayWrapper__ = __webpack_require__(12);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DayWrapper", function() { return __WEBPACK_IMPORTED_MODULE_22__vowrapper_DayWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_23__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosageWrapper", function() { return __WEBPACK_IMPORTED_MODULE_23__vowrapper_DosageWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_24__vowrapper_DoseWrapper__ = __webpack_require__(4);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_24__vowrapper_DoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_25__vowrapper_EveningDoseWrapper__ = __webpack_require__(19);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "EveningDoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_25__vowrapper_EveningDoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_26__vowrapper_FreeTextWrapper__ = __webpack_require__(20);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "FreeTextWrapper", function() { return __WEBPACK_IMPORTED_MODULE_26__vowrapper_FreeTextWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_27__vowrapper_MorningDoseWrapper__ = __webpack_require__(21);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "MorningDoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_27__vowrapper_MorningDoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_28__vowrapper_NightDoseWrapper__ = __webpack_require__(22);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "NightDoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_28__vowrapper_NightDoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_29__vowrapper_NoonDoseWrapper__ = __webpack_require__(23);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "NoonDoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_29__vowrapper_NoonDoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_30__vowrapper_PlainDoseWrapper__ = __webpack_require__(24);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "PlainDoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_30__vowrapper_PlainDoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_31__vowrapper_StructuresWrapper__ = __webpack_require__(7);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "StructuresWrapper", function() { return __WEBPACK_IMPORTED_MODULE_31__vowrapper_StructuresWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_32__vowrapper_StructureWrapper__ = __webpack_require__(13);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "StructureWrapper", function() { return __WEBPACK_IMPORTED_MODULE_32__vowrapper_StructureWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_33__vowrapper_TimedDoseWrapper__ = __webpack_require__(48);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "TimedDoseWrapper", function() { return __WEBPACK_IMPORTED_MODULE_33__vowrapper_TimedDoseWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_34__vowrapper_UnitOrUnitsWrapper__ = __webpack_require__(25);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "UnitOrUnitsWrapper", function() { return __WEBPACK_IMPORTED_MODULE_34__vowrapper_UnitOrUnitsWrapper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_35__DosisTilTekstException__ = __webpack_require__(5);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosisTilTekstException", function() { return __WEBPACK_IMPORTED_MODULE_35__DosisTilTekstException__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_36__Factory__ = __webpack_require__(54);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "Factory", function() { return __WEBPACK_IMPORTED_MODULE_36__Factory__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_37__LocalTime__ = __webpack_require__(31);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "LocalTime", function() { return __WEBPACK_IMPORTED_MODULE_37__LocalTime__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_38__LoggerService__ = __webpack_require__(55);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "LoggerService", function() { return __WEBPACK_IMPORTED_MODULE_38__LoggerService__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_39__LongTextConverter__ = __webpack_require__(10);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "LongTextConverter", function() { return __WEBPACK_IMPORTED_MODULE_39__LongTextConverter__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_40__ShortTextConverter__ = __webpack_require__(8);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "ShortTextConverter", function() { return __WEBPACK_IMPORTED_MODULE_40__ShortTextConverter__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_41__CombinedTextConverter__ = __webpack_require__(51);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "CombinedTextConverter", function() { return __WEBPACK_IMPORTED_MODULE_41__CombinedTextConverter__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_42__TextHelper__ = __webpack_require__(0);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "TextHelper", function() { return __WEBPACK_IMPORTED_MODULE_42__TextHelper__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_43__Validator__ = __webpack_require__(32);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "Validator", function() { return __WEBPACK_IMPORTED_MODULE_43__Validator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_44__DosageTypeCalculator__ = __webpack_require__(30);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosageTypeCalculator", function() { return __WEBPACK_IMPORTED_MODULE_44__DosageTypeCalculator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_45__DosageTypeCalculator144__ = __webpack_require__(53);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosageTypeCalculator144", function() { return __WEBPACK_IMPORTED_MODULE_45__DosageTypeCalculator144__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_46__DailyDosisCalculator__ = __webpack_require__(26);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DailyDosisCalculator", function() { return __WEBPACK_IMPORTED_MODULE_46__DailyDosisCalculator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_47__DosageProposalXMLGenerator_DosageProposalXMLGenerator__ = __webpack_require__(52);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosageProposalXMLGenerator", function() { return __WEBPACK_IMPORTED_MODULE_47__DosageProposalXMLGenerator_DosageProposalXMLGenerator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_48__DosageProposalXMLGenerator_DosageProposalXML__ = __webpack_require__(28);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosageProposalXML", function() { return __WEBPACK_IMPORTED_MODULE_48__DosageProposalXMLGenerator_DosageProposalXML__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_49__DosageProposalXMLGenerator_XML140Generator__ = __webpack_require__(14);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "XML140Generator", function() { return __WEBPACK_IMPORTED_MODULE_49__DosageProposalXMLGenerator_XML140Generator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_50__DosageProposalXMLGenerator_XML142Generator__ = __webpack_require__(15);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "XML142Generator", function() { return __WEBPACK_IMPORTED_MODULE_50__DosageProposalXMLGenerator_XML142Generator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_51__DosageProposalXMLGenerator_XML144Generator__ = __webpack_require__(16);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "XML144Generator", function() { return __WEBPACK_IMPORTED_MODULE_51__DosageProposalXMLGenerator_XML144Generator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_52__DosageProposalXMLGenerator_XML146Generator__ = __webpack_require__(29);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "XML146Generator", function() { return __WEBPACK_IMPORTED_MODULE_52__DosageProposalXMLGenerator_XML146Generator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_53__DosageProposalXMLGenerator_DosagePeriod__ = __webpack_require__(27);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "DosagePeriod", function() { return __WEBPACK_IMPORTED_MODULE_53__DosageProposalXMLGenerator_DosagePeriod__["a"]; });
// This file exposes all modules
// ./longtextconverterimpl










// ./shorttextconverterimpl









// ./vowrapper
















// ./












// ./DosageProposalXMLGenerator









/***/ }),
/* 60 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CombinedTwoPeriodesConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__ = __webpack_require__(8);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__vowrapper_DosageWrapper__ = __webpack_require__(2);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__vowrapper_StructuresWrapper__ = __webpack_require__(7);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();




var CombinedTwoPeriodesConverterImpl = (function (_super) {
    __extends(CombinedTwoPeriodesConverterImpl, _super);
    function CombinedTwoPeriodesConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CombinedTwoPeriodesConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 2)
            return false;
        // Structure 0
        var structure0 = dosage.structures.getStructures()[0];
        if (structure0.getIterationInterval() !== 0)
            return false;
        if (structure0.containsAccordingToNeedDose())
            return false;
        var tempDosage = new __WEBPACK_IMPORTED_MODULE_2__vowrapper_DosageWrapper__["a" /* DosageWrapper */](undefined, undefined, new __WEBPACK_IMPORTED_MODULE_3__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](dosage.structures.getUnitOrUnits(), [structure0]));
        if (!__WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__["a" /* ShortTextConverter */].getInstance().canConvert(tempDosage))
            return false;
        // Structure 1
        var structureLast = dosage.structures.getStructures()[dosage.structures.getStructures().length - 1];
        if (structureLast.containsAccordingToNeedDose())
            return false;
        var fixedDosage = new __WEBPACK_IMPORTED_MODULE_2__vowrapper_DosageWrapper__["a" /* DosageWrapper */](undefined, undefined, new __WEBPACK_IMPORTED_MODULE_3__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](dosage.structures.getUnitOrUnits(), [structureLast]));
        if (!__WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__["a" /* ShortTextConverter */].getInstance().canConvert(fixedDosage))
            return false;
        return true;
    };
    CombinedTwoPeriodesConverterImpl.prototype.doConvert = function (dosage) {
        var tempStructure = dosage.structures.getStructures()[0];
        var tempDosage = new __WEBPACK_IMPORTED_MODULE_2__vowrapper_DosageWrapper__["a" /* DosageWrapper */](undefined, undefined, new __WEBPACK_IMPORTED_MODULE_3__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](dosage.structures.getUnitOrUnits(), [tempStructure]));
        var tempText = new __WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__["a" /* ShortTextConverter */]().convertWrapper(tempDosage);
        var fixedStructure = dosage.structures.getStructures()[dosage.structures.getStructures().length - 1];
        var fixedDosage = new __WEBPACK_IMPORTED_MODULE_2__vowrapper_DosageWrapper__["a" /* DosageWrapper */](undefined, undefined, new __WEBPACK_IMPORTED_MODULE_3__vowrapper_StructuresWrapper__["a" /* StructuresWrapper */](dosage.structures.getUnitOrUnits(), [fixedStructure]));
        var fixedText = new __WEBPACK_IMPORTED_MODULE_1__ShortTextConverter__["a" /* ShortTextConverter */]().convertWrapper(fixedDosage);
        var days = tempStructure.getDays()[tempStructure.getDays().length - 1].getDayNumber();
        if (days === 1) {
            return "første dag " + tempText + ", herefter " + fixedText;
        }
        else {
            var tempTail = undefined;
            if (days === 7)
                tempTail = " i 1 uge";
            else if (days % 7 === 0)
                tempTail = " i " + (days / 7) + " uger";
            else
                tempTail = " i " + days + " dage";
            if (tempTail && tempText.indexOf(tempTail) > 0)
                return tempText + ", herefter " + fixedText;
            else
                return tempText + tempTail + ", herefter " + fixedText;
        }
    };
    return CombinedTwoPeriodesConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 61 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DayInWeekConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var DayInWeekConverterImpl = (function (_super) {
    __extends(DayInWeekConverterImpl, _super);
    function DayInWeekConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    DayInWeekConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() % 7 > 0)
            return false;
        if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
            return false;
        if (structure.getDays().length < 2)
            return false;
        // Check there is only one day in each week
        var daysAsList = structure.getDays();
        for (var week = 0; week < daysAsList.length; week++) {
            if (structure.getDays()[week].getDayNumber() < (week * 7 + 1))
                return false;
            if (structure.getDays()[week].getDayNumber() > (week * 7 + 7))
                return false;
        }
        if (!structure.sameDayOfWeek())
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        if (!structure.allDosesAreTheSame())
            return false;
        if (structure.containsAccordingToNeedDose() || structure.containsTimedDose())
            return false;
        return true;
    };
    DayInWeekConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        // Append dosage
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        // Add times daily
        if (day.getNumberOfDoses() > 1)
            text += " " + day.getNumberOfDoses() + " gange daglig ";
        else
            text += " daglig ";
        text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].makeDayOfWeekAndName(structure.getStartDateOrDateTime(), day, false).getName();
        if (structure.getSupplText()) {
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        }
        var weeks = structure.getDays().length;
        var pauseWeeks = structure.getIterationInterval() / 7 - weeks;
        // If pause == 0 then this structure is equivalent to a structure with just one day and iteration=1
        if (pauseWeeks > 0) {
            // Add how many weeks/days
            if (weeks === 1) {
                text += " i første uge";
            }
            else {
                text += " i de første " + weeks + " uger";
            }
            // Add pause
            if (pauseWeeks === 1) {
                text += ", herefter 1 uges pause";
            }
            else {
                text += ", herefter " + pauseWeeks + " ugers pause";
            }
        }
        return text;
    };
    return DayInWeekConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 62 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LimitedNumberOfDaysConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


/**
 * Conversion of: Dosage limited to N days, the same every day
 * <p>
 * Example:<br>
 * 67: 3 tabletter 4 gange daglig i 3 dage<br>
 * 279: 2 tabletter 2 gange daglig i 6 dage
 */
var LimitedNumberOfDaysConverterImpl = (function (_super) {
    __extends(LimitedNumberOfDaysConverterImpl, _super);
    function LimitedNumberOfDaysConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    LimitedNumberOfDaysConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 0)
            return false;
        if (structure.getDays().length === 0)
            return false;
        if (!structure.daysAreInUninteruptedSequenceFromOne())
            return false;
        if (structure.containsMorningNoonEveningNightDoses())
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        if (!structure.allDosesAreTheSame())
            return false;
        return true;
    };
    LimitedNumberOfDaysConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        if (day.getAllDoses()[0].getIsAccordingToNeed()) {
            text += " efter behov";
        }
        if (structure.getDays().length === 1 && structure.getDays()[0].getDayNumber() === 1)
            text += " " + day.getAllDoses().length + " " + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].gange(day.getAllDoses().length);
        else {
            text += " " + day.getAllDoses().length + " " + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].gange(day.getAllDoses().length) + " daglig";
            var days = structure.getDays()[structure.getDays().length - 1].getDayNumber();
            if (days === 7)
                text += " i 1 uge";
            else if (days % 7 === 0)
                text += " i " + (days / 7) + " uger";
            else
                text += " i " + days + " dage";
        }
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text.toString();
    };
    return LimitedNumberOfDaysConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 63 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MorningNoonEveningNightInNDaysConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__ = __webpack_require__(9);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


/**
 * Conversion of: Non repeated morning, noon, evening, night-dosage where all dosages are equal
 */
var MorningNoonEveningNightInNDaysConverterImpl = (function (_super) {
    __extends(MorningNoonEveningNightInNDaysConverterImpl, _super);
    function MorningNoonEveningNightInNDaysConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MorningNoonEveningNightInNDaysConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 0)
            return false;
        if (structure.getDays().length < 2)
            return false;
        if (structure.startsAndEndsSameDay())
            return false;
        if (structure.containsPlainDose())
            return false;
        if (structure.containsTimedDose())
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        if (!structure.daysAreInUninteruptedSequenceFromOne())
            return false;
        return true;
    };
    MorningNoonEveningNightInNDaysConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getMorningText(day, dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getNoonText(day, dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getEveningText(day, dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getNightText(day, dosage.structures.getUnitOrUnits());
        text += __WEBPACK_IMPORTED_MODULE_1__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getSupplText(structure.getSupplText());
        text += (" i " + dosage.structures.getStructures()[0].getDays()[dosage.structures.getStructures()[0].getDays().length - 1].getDayNumber() + " dage");
        return text;
    };
    return MorningNoonEveningNightInNDaysConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 64 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MultipleDaysNonRepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__MorningNoonEveningNightConverterImpl__ = __webpack_require__(9);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var MultipleDaysNonRepeatedConverterImpl = (function (_super) {
    __extends(MultipleDaysNonRepeatedConverterImpl, _super);
    function MultipleDaysNonRepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MultipleDaysNonRepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 0)
            return false;
        if (structure.getDays().length <= 1)
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        if (!structure.allDosesAreTheSame())
            return false;
        if (structure.containsAccordingToNeedDose())
            return false;
        if (structure.containsTimedDose())
            return false;
        if (structure.containsMorningNoonEveningNightDoses() && structure.containsPlainDose())
            return false;
        return true;
    };
    MultipleDaysNonRepeatedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var firstDay = structure.getDays()[0];
        if (structure.containsMorningNoonEveningNightDoses()) {
            text += __WEBPACK_IMPORTED_MODULE_2__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getMorningText(firstDay, dosage.structures.getUnitOrUnits());
            __WEBPACK_IMPORTED_MODULE_2__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getNoonText(firstDay, dosage.structures.getUnitOrUnits());
            __WEBPACK_IMPORTED_MODULE_2__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getEveningText(firstDay, dosage.structures.getUnitOrUnits());
            __WEBPACK_IMPORTED_MODULE_2__MorningNoonEveningNightConverterImpl__["a" /* MorningNoonEveningNightConverterImpl */].getNightText(firstDay, dosage.structures.getUnitOrUnits());
        }
        else {
            text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(firstDay.getDose(0), dosage.structures.getUnitOrUnits());
            text += " " + firstDay.getAllDoses().length + " " + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].gange(firstDay.getAllDoses().length) + " daglig";
        }
        for (var i = 0; i < structure.getDays().length; i++) {
            var day = structure.getDays()[i];
            if (i === 0)
                text += " dag " + day.getDayNumber();
            else if (i === structure.getDays().length - 1)
                text += " og " + day.getDayNumber();
            else
                text += ", " + day.getDayNumber();
        }
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text.toString();
    };
    return MultipleDaysNonRepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 65 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NumberOfWholeWeeksConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var NumberOfWholeWeeksConverterImpl = (function (_super) {
    __extends(NumberOfWholeWeeksConverterImpl, _super);
    function NumberOfWholeWeeksConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    NumberOfWholeWeeksConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() % 7 > 0)
            return false;
        if (structure.getStartDateOrDateTime().isEqualTo(structure.getEndDateOrDateTime()))
            return false;
        if (structure.getDays().length === 0)
            return false;
        if (structure.getDays()[0].getDayNumber() > 7)
            return false;
        if (!structure.daysAreInUninteruptedSequenceFromOne())
            return false;
        if (!structure.allDaysAreTheSame())
            return false;
        if (!structure.allDosesAreTheSame())
            return false;
        if (structure.containsAccordingToNeedDose() || structure.containsTimedDose())
            return false;
        return true;
    };
    NumberOfWholeWeeksConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        // Append dosage
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        // Add times daily
        if (day.getNumberOfDoses() > 1)
            text += " " + day.getNumberOfDoses() + " gange daglig";
        else
            text += " daglig";
        if (structure.getSupplText()) {
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        }
        var days = structure.getDays().length;
        var pauseDays = structure.getIterationInterval() - days;
        // If pause == 0 then this structure is equivalent to a structure with just one day and iteration=1
        if (pauseDays > 0) {
            // Add how many weeks/days
            if (days === 7) {
                text += " i en uge";
            }
            else if (days % 7 === 0) {
                var weeks = days / 7;
                text += " i " + weeks + " uger";
            }
            else {
                text += " i " + days + " dage";
            }
            // Add pause
            if (pauseDays === 7) {
                text += ", herefter en uges pause";
            }
            else if (pauseDays % 7 === 0) {
                var pauseWeeks = pauseDays / 7;
                text += ", herefter " + pauseWeeks + " ugers pause";
            }
            else if (pauseDays === 1) {
                text += ", herefter 1 dags pause";
            }
            else {
                text += ", herefter " + pauseDays + " dages pause";
            }
        }
        return text;
    };
    return NumberOfWholeWeeksConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 66 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ParacetamolConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var ParacetamolConverterImpl = (function (_super) {
    __extends(ParacetamolConverterImpl, _super);
    function ParacetamolConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ParacetamolConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (!day.containsAccordingToNeedDose())
            return false;
        if (day.containsAccordingToNeedDosesOnly())
            return false;
        if (day.containsTimedDose()) {
            return false;
        }
        if (!day.containsPlainDose())
            return false;
        if (day.getMorningDose() || day.getNoonDose()
            || day.getEveningDose() || day.getNightDose())
            return false;
        if (!day.allDosesHaveTheSameQuantity())
            return false;
        return true;
    };
    ParacetamolConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        text += " " + (day.getNumberOfPlainDoses() - day.getNumberOfAccordingToNeedDoses()) + "-" + (day.getNumberOfPlainDoses());
        text += " gange daglig";
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text.toString();
    };
    return ParacetamolConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 67 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return RepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var RepeatedConverterImpl = (function (_super) {
    __extends(RepeatedConverterImpl, _super);
    function RepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    RepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() === 0)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.containsAccordingToNeedDose())
            return false;
        if (!day.allDosesAreTheSame())
            return false;
        return true;
    };
    RepeatedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        // Append dosage
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        // Append iteration:
        text += this.makeIteration(structure, day);
        // Append suppl. text
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text;
    };
    RepeatedConverterImpl.prototype.makeIteration = function (structure, day) {
        var iterationInterval = structure.getIterationInterval();
        var numberOfDoses = day.getNumberOfDoses();
        // Repeated daily
        if (iterationInterval === 1 && numberOfDoses === 1) {
            if (day.getDose(0).getLabel() === "" && day.getDose(0).getDoseQuantity() && day.getDose(0).getDoseQuantity() > 1.000000001)
                return " 1 gang daglig";
            else
                return " daglig";
        }
        if (iterationInterval === 1 && numberOfDoses > 1)
            return " " + numberOfDoses + " " + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].gange(numberOfDoses) + " daglig";
        // Repeated monthly
        var numberOfWholeMonths = this.calculateNumberOfWholeMonths(iterationInterval);
        if (numberOfWholeMonths === 1 && numberOfDoses === 1)
            return " 1 gang om måneden";
        if (numberOfWholeMonths === 1 && numberOfDoses >= 1)
            return " " + numberOfDoses + " " + "gange samme dag 1 gang om måneden";
        if (numberOfWholeMonths > 1 && numberOfDoses === 1)
            return " hver " + numberOfWholeMonths + ". måned";
        // Repeated weekly
        var numberOfWholeWeeks = this.calculateNumberOfWholeWeeks(structure.getIterationInterval());
        var name = __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].makeDayOfWeekAndName(structure.getStartDateOrDateTime(), day, false).getName();
        if (numberOfWholeWeeks === 1 && day.getNumberOfDoses() === 1)
            return " " + name + " hver uge";
        else if (numberOfWholeWeeks === 1 && numberOfDoses > 1)
            return " " + numberOfDoses + " " + "gange " + name + " hver uge";
        if (numberOfWholeWeeks > 1 && numberOfDoses === 1)
            return " " + name + " hver " + numberOfWholeWeeks + ". uge";
        // Every Nth day
        if (iterationInterval > 1 && numberOfDoses === 1)
            return " hver " + iterationInterval + ". dag";
        if (iterationInterval > 1 && numberOfDoses >= 1)
            return " " + numberOfDoses + " " + "gange samme dag hver " + iterationInterval + ". dag";
        // Above is exhaustive if both iterationInterval>1 and numberOfDoses>1, return null to make compiler happy
        return null;
    };
    return RepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 68 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return RepeatedEyeOrEarConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var RepeatedEyeOrEarConverterImpl = (function (_super) {
    __extends(RepeatedEyeOrEarConverterImpl, _super);
    function RepeatedEyeOrEarConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    RepeatedEyeOrEarConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 1)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.getDayNumber() !== 1)
            return false;
        if (day.containsTimedDose())
            return false;
        if (day.containsAccordingToNeedDose())
            return false;
        if (!day.allDosesAreTheSame())
            return false;
        if (day.getAllDoses()[0].getDoseQuantity() === undefined)
            return false;
        if (!__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].hasIntegerValue(day.getAllDoses()[0].getDoseQuantity()))
            return false;
        var quantity = day.getAllDoses()[0].getDoseQuantity();
        if (!(quantity % 2 === 0))
            return false;
        if (structure.getSupplText() === undefined)
            return false;
        if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øje")) {
            if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strStartsWith(structure.getSupplText(), ",")) {
                if (structure.getSupplText() !== (", " + (quantity / 2) + " i hvert øje"))
                    return false;
            }
            else {
                if (structure.getSupplText() !== ((quantity / 2) + " i hvert øje"))
                    return false;
            }
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øre")) {
            if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strStartsWith(structure.getSupplText(), ",")) {
                if (structure.getSupplText() !== (", " + (quantity / 2) + " i hvert øre"))
                    return false;
            }
            else {
                if (structure.getSupplText() !== ((quantity / 2) + " i hvert øre"))
                    return false;
            }
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert næsebor")) {
            if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strStartsWith(structure.getSupplText(), ",")) {
                if (structure.getSupplText() !== (", " + (quantity / 2) + " i hvert næsebor"))
                    return false;
            }
            else {
                if (structure.getSupplText() !== ((quantity / 2) + " i hvert næsebor"))
                    return false;
            }
        }
        else {
            return false;
        }
        return true;
    };
    RepeatedEyeOrEarConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        // Append dosage
        var day = structure.getDays()[0];
        text += (__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseLabelUnitValue(day.getAllDoses()[0].getDoseQuantity() / 2, day.getAllDoses()[0].getLabel(), dosage.structures.getUnitOrUnits()));
        // Append iteration:
        text += this.makeIteration(structure, day);
        // Append suppl. text
        var supplText = "";
        if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øje")) {
            supplText = " i begge øjne";
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert øre")) {
            supplText = " i begge ører";
        }
        else if (__WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].strEndsWith(structure.getSupplText(), "i hvert næsebor")) {
            supplText = " i begge næsebor";
        }
        text += supplText;
        return text;
    };
    RepeatedEyeOrEarConverterImpl.prototype.makeIteration = function (structure, day) {
        var iterationInterval = structure.getIterationInterval();
        var numberOfDoses = day.getNumberOfDoses();
        // Repeated daily
        if (iterationInterval === 1 && numberOfDoses === 1)
            return " daglig";
        if (iterationInterval === 1 && numberOfDoses > 1)
            return " " + numberOfDoses + " " + __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].gange(numberOfDoses) + " daglig";
        // Repeated monthly
        var numberOfWholeMonths = this.calculateNumberOfWholeMonths(iterationInterval);
        if (numberOfWholeMonths === 1 && numberOfDoses === 1)
            return " 1 gang om måneden";
        if (numberOfWholeMonths === 1 && numberOfDoses >= 1)
            return " " + numberOfDoses + " " + "gange samme dag 1 gang om måneden";
        if (numberOfWholeMonths > 1 && numberOfDoses === 1)
            return " hver " + numberOfWholeMonths + ". måned";
        // Repeated weekly
        var numberOfWholeWeeks = this.calculateNumberOfWholeWeeks(structure.getIterationInterval());
        var name = __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].makeDayOfWeekAndName(structure.getStartDateOrDateTime(), day, false).getName();
        if (numberOfWholeWeeks === 1 && day.getNumberOfDoses() === 1)
            return " " + name + " hver uge";
        else if (numberOfWholeWeeks === 1 && numberOfDoses > 1)
            return " " + numberOfDoses + " " + "gange " + name + " hver uge";
        if (numberOfWholeWeeks > 1 && numberOfDoses === 1)
            return " " + name + " hver " + numberOfWholeWeeks + ". uge";
        // Every Nth day
        if (iterationInterval > 1 && numberOfDoses === 1)
            return " hver " + iterationInterval + ". dag";
        if (iterationInterval > 1 && numberOfDoses >= 1)
            return " " + numberOfDoses + " " + "gange samme dag hver " + iterationInterval + ". dag";
        // Above is exhaustive if both iterationInterval>1 and numberOfDoses>1, return null to make compiler happy
        return null;
    };
    return RepeatedEyeOrEarConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 69 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SimpleAccordingToNeedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__TextHelper__ = __webpack_require__(0);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


/**
 * Conversion of simple "according to need" dosage, with or without supplementary dosage free text
 * <p>
 * Example<br>
 * 2: 2 stk efter behov
 */
var SimpleAccordingToNeedConverterImpl = (function (_super) {
    __extends(SimpleAccordingToNeedConverterImpl, _super);
    function SimpleAccordingToNeedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SimpleAccordingToNeedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 0)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (!day.containsAccordingToNeedDosesOnly())
            return false;
        if (day.getAccordingToNeedDoses().length > 1)
            return false;
        return true;
    };
    SimpleAccordingToNeedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(day.getAllDoses()[0], dosage.structures.getUnitOrUnits());
        text += " efter behov";
        if (structure.getSupplText())
            text += __WEBPACK_IMPORTED_MODULE_1__TextHelper__["a" /* TextHelper */].maybeAddSpace(structure.getSupplText()) + structure.getSupplText();
        return text.toString();
    };
    return SimpleAccordingToNeedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ }),
/* 70 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SimpleNonRepeatedConverterImpl; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__ = __webpack_require__(1);
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

/**
 * Conversion of: Simple non repeated dosage (like "according to need") with suppl.
 * dosage free text. All dosages the same.
 * <p>
 * Example:<br>
 * 204: 1 plaster 5 timer før virkning ønskes,
 */
var SimpleNonRepeatedConverterImpl = (function (_super) {
    __extends(SimpleNonRepeatedConverterImpl, _super);
    function SimpleNonRepeatedConverterImpl() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SimpleNonRepeatedConverterImpl.prototype.canConvert = function (dosage) {
        if (dosage.structures === undefined)
            return false;
        if (dosage.structures.getStructures().length !== 1)
            return false;
        var structure = dosage.structures.getStructures()[0];
        if (structure.getIterationInterval() !== 0)
            return false;
        if (structure.getDays().length !== 1)
            return false;
        var day = structure.getDays()[0];
        if (day.getDayNumber() !== 0 && (!(structure.startsAndEndsSameDay() && day.getDayNumber() === 1)))
            return false;
        if (day.containsAccordingToNeedDose() || day.containsMorningNoonEveningNightDoses())
            return false;
        if (day.getNumberOfDoses() !== 1)
            return false;
        return true;
    };
    SimpleNonRepeatedConverterImpl.prototype.doConvert = function (dosage) {
        var structure = dosage.structures.getStructures()[0];
        var text = "";
        var day = structure.getDays()[0];
        var dose = day.getAllDoses()[0];
        text += __WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */].toDoseAndUnitValue(dose, dosage.structures.getUnitOrUnits());
        if (structure.getSupplText())
            text += " " + structure.getSupplText();
        return text;
    };
    return SimpleNonRepeatedConverterImpl;
}(__WEBPACK_IMPORTED_MODULE_0__ShortTextConverterImpl__["a" /* ShortTextConverterImpl */]));



/***/ })
/******/ ]);
//# sourceMappingURL=dosistiltekst.js.map