#r "..\bin\Debug\netcoreapp2.1\advanced_plugin.dll"

var result = new advanced_plugin.ValidationResult{
    IsValid = true
};
if (Weight > 100) {
    result.IsValid = false;
    result.ErrorText = "Weight can not be above 100.";
}
result