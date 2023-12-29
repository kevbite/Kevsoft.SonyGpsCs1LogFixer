using System.Globalization;

var dateShift = TimeSpan.FromDays(256);
var sourceFolder = args[0];
var destinationFolder = args[1];

var files = Directory.GetFiles(sourceFolder, "*.log", SearchOption.TopDirectoryOnly);

foreach (var file in files)
{
    var fileName = Path.GetFileName(file);
    Console.WriteLine($"Processing file: {fileName}");
    
    var lines = File.ReadAllLines(file);
    var startDate = (DateTime?)null;
    for (var i = 0; i < lines.Length - 1; i++)
    {
        var line = lines[i];
        if (line.StartsWith("$GPRMC"))
        {
            var arguments = line[1..line.LastIndexOf("*", StringComparison.InvariantCulture)].Split(",");
            var dateArgument = arguments[9];
            var date = DateTime.ParseExact(dateArgument, "yyMMdd", CultureInfo.InvariantCulture);
            startDate ??= date = date.Add(dateShift);
            arguments[9] = date.ToString("yyMMdd", CultureInfo.InvariantCulture);

            line = string.Join(",", arguments);

            // Compute the checksum by XORing all the character values in the string.
            var checksum = line.Aggregate(0, (checksum, c) => checksum ^ c);
            line = $"${line}*{checksum:X2}";
            lines[i] = line;
        }
    }

    var newFilename = fileName[..2] + startDate!.Value.ToString("yyyyMMdd") + fileName[10..];
    Console.WriteLine($"Writing file: {newFilename}");
    File.WriteAllLines(Path.Combine(destinationFolder, newFilename), lines);
}


Console.WriteLine($"Completed {files.Length} files");