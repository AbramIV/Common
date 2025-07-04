namespace Exercise.JSON;

public class Rootobject
{
    public Class1[] Property1 { get; set; }
}

public class Class1
{
    public Passport_Registration passport_registration { get; set; }
    public Passport_Biographical passport_biographical { get; set; }
    public License_Back license_back { get; set; }
    public License_Front license_front { get; set; }
    public Passport_Selfie passport_selfie { get; set; }
}

public class Passport_Registration
{
    public Data data { get; set; }
    public Confidence confidence { get; set; }
    public bool is_verified { get; set; }
}

public class Data
{
    public string country { get; set; }
    public object housing { get; set; }
    public object locality { get; set; }
    public string area { get; set; }
    public string house { get; set; }
    public string region { get; set; }
    public string street { get; set; }
    public string apartment { get; set; }
    public object registration_expiration_date { get; set; }
}

public class Confidence
{
    public int area { get; set; }
    public int house { get; set; }
    public int region { get; set; }
    public int street { get; set; }
}

public class Passport_Biographical
{
    public Data1 data { get; set; }
    public Confidence1 confidence { get; set; }
    public bool is_verified { get; set; }
}

public class Data1
{
    public string country { get; set; }
    public string birth_place { get; set; }
    public string first_name { get; set; }
    public string subdivision_code { get; set; }
    public string gender { get; set; }
    public string middle_name { get; set; }
    public string last_name { get; set; }
    public string citizenship { get; set; }
    public string birth_date { get; set; }
    public string number { get; set; }
    public string issue_date { get; set; }
}

public class Confidence1
{
    public float first_name { get; set; }
    public float last_name { get; set; }
    public float issue_date { get; set; }
}

public class License_Back
{
    public Data2 data { get; set; }
    public bool is_verified { get; set; }
}

public class Data2
{
    public string country { get; set; }
    public string number { get; set; }
    public string categories_b_valid_from_date { get; set; }
    public string categories_b_valid_to_date { get; set; }
    public string prev_licence_issue_date { get; set; }
    public string prev_licence_number { get; set; }
    public string experience_from { get; set; }
    public bool has_at_mark { get; set; }
}

public class License_Front
{
    public Data3 data { get; set; }
    public Confidence2 confidence { get; set; }
    public bool is_verified { get; set; }
}

public class Data3
{
    public string country { get; set; }
    public string first_name { get; set; }
    public string middle_name { get; set; }
    public string last_name { get; set; }
    public string birth_date { get; set; }
    public string number { get; set; }
    public string issue_date { get; set; }
    public string categories { get; set; }
}

public class Confidence2
{
    public float first_name { get; set; }
    public float issue_date { get; set; }
}

public class Passport_Selfie
{
    public bool is_verified { get; set; }
}