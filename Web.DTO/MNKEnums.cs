
using System.Collections.Generic;

namespace Web.DTO.Enums
{
    public class MNKEnum
    {
        private int _id;
        private string _value;
        private string _displayText;

        public MNKEnum(int id, string value, string displayText = "")
        {
            this.Id = id ;
            this.Value = value ?? string.Empty;
            this.DisplayText = displayText ?? string.Empty;
        }
        public int Id { get => _id; private set => _id = value; }
        public string Value { get => _value.ToUpper(); private set => _value = value; }
        public string DisplayText { get => _displayText.ToUpper(); private set => _displayText = value; }
    }

    public class AddressTypeEnums
    {
        public static readonly MNKEnum Home = new MNKEnum(10, "Home");
        public static readonly MNKEnum Work = new MNKEnum(20, "Work");
        public static readonly MNKEnum Other = new MNKEnum(30, "Other");

        public static List<MNKEnum> ToList()
        {
            return new List<MNKEnum>{Home, Work, Other};
        }

        public static bool IsValid(string addressType)
        {
            bool ret = false;
            if (!string.IsNullOrWhiteSpace(addressType))
            {
                var addr = addressType.ToUpper();
                ret = ((addr == Home.Value) || (addr == Work.Value) || (addr == Other.Value));
            }
            return ret;
        }
    }

    public enum RelationshipStatusEnum
    {
        Spouse = 10,
        Partner = 20,
        Mother = 30,
        Father = 40,
        Sister = 50,
        Brother = 60,
        Son = 70,
        Daughter = 80,
        Friend = 90,
        Other = 100
    }

    public enum MaritalStatusEnum
    {
        Married = 10,
        Single = 20,
        Divorced = 30,
        Separated = 40,
        Widowed = 50,
        Domestic_Partner = 60
    }

    public enum GenderEnum
    {
        Female = 10,
        Male = 20,
        Other = 30
    }

    public enum EthnicityEnum
    {
        American_Indian_OR_Alaska_Native = 10,
        Asian = 20,
        Black_OR_African_American = 30,
        Hawaiian_OR_Pacific_Islander = 40,
        Hispanic_OR_Latino = 50,
        White = 60,
        Two_OR_More_Races = 70
    }

    public enum EmploymentTypeEnum
    {
        Regular = 10,
        Officer = 20,
        Statutory = 30,
        Owner = 40
    }

    public enum StateEnum
    {
        AL = 10,
        AK = 20,
        AZ = 30,
        AR = 40,
        CA = 50,
        CO = 60,
        CT = 70,
        DE = 80,
        DC = 90,
        FL = 100,
        GA = 110,
        HI = 120,
        ID = 130,
        IL = 140,
        IN = 150,
        IA = 160,
        KS = 170,
        KY = 180,
        LA = 190,
        ME = 200,
        MD = 210,
        MA = 220,
        MI = 230,
        MN = 240,
        MS = 250,
        MO = 260,
        MT = 270,
        NE = 280,
        NV = 290,
        NH = 300,
        NJ = 310,
        NM = 320,
        NY = 330,
        NC = 340,
        ND = 350,
        OH = 360,
        OK = 370,
        OR = 380,
        PA = 390,
        PR = 400,
        RI = 410,
        SC = 420,
        SD = 430,
        TN = 440,
        TX = 450,
        UT = 460,
        VT = 470,
        VA = 480,
        WA = 490,
        WV = 500,
        WI = 510,
        WY = 520
    }

    public enum StateFullEnum
    {
        
        Alabama = 10,
        Alaska = 20,
        Arizona = 30,
        Arkansas = 40,
        California = 50,
        Colorado = 60,
        Connecticut = 70,
        Delaware = 80,
        District_of_Columbia = 90,
        Florida = 100,
        Georgia = 110,
        Hawaii = 120,
        Idaho = 130,
        Illinois = 140,
        Indiana = 150,
        Iowa = 160,
        Kansas = 170,
        Kentucky = 180,
        Louisiana = 190,
        Maine = 200,
        Maryland = 210,
        Massachusetts = 220,
        Michigan = 230,
        Minnesota = 240,
        Mississippi = 250,
        Missouri = 260,
        Montana = 270,
        Nebraska = 280,
        Nevada = 290,
        New_Hampshire = 300,
        New_Jersey = 310,
        New_Mexico = 320,
        New_York = 330,
        North_Carolina = 340,
        North_Dakota = 350,
        Ohio = 360,
        Oklahoma = 370,
        Oregon = 380,
        Pennsylvania = 390,
        Puerto_Rico = 400,
        Rhode_Island = 410,
        South_Carolina = 420,
        South_Dakota = 430,
        Tennessee = 440,
        Texas = 450,
        Utah = 460,
        Vermont = 470,
        Virginia = 480,
        Washington = 490,
        West_Virginia = 500,
        Wisconsin = 510,
        Wyoming = 520,
    }
    public enum ImmigrationStatusEnum
    {
        
        H1B = 10,
        Citizen = 20,

        Green_Card = 30,
        EAD = 40,
        OPT_EAD = 50,

        CPT_EAD = 60,

        L1 = 70,
        L2 = 80
    }

    public enum RatePerEnum
    {
        Hourly = 10,
        Yearly = 20,
        Monthly = 30
    }
}

