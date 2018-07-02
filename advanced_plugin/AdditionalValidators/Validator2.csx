#r "..\bin\Debug\netcoreapp2.1\advanced_plugin.dll"

var result = new advanced_plugin.ValidationResult{
    IsValid = true
};
if (Destination != "Switzerland") {
    result.IsValid = false;
    result.ErrorText = "Destination must be Switzerland.";
}
result