#r "..\bin\Debug\netcoreapp2.1\advanced_plugin.dll"

var result = new advanced_plugin.ValidationResult{
    IsValid = true
};
if (Cost < 150) {
    result.IsValid = false;
    result.ErrorText = "Cost can not be below 150.";
}
result