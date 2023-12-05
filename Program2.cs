Console.WriteLine("Введите количество строк матрицы:");
var isValidrows = int.TryParse(Console.ReadLine(), out var rows);
if (!isValidrows || rows <= 0)
{
    Console.WriteLine("Количествово строк массива должен быть целым положительным числом");
    return;
}

Console.WriteLine("Введите количество столбцов матрицы:");
var isValidcols = int.TryParse(Console.ReadLine(), out var columns);
if (!isValidcols || columns <= 0)
{
    Console.WriteLine("Количествово столбцов массива должен быть целым положительным числом");
    return;
}

var matrix = new int[rows, columns];
var random = new Random();

// Заполнение матрицы случайными значениями от -256 до 256
for (var i = 0; i < rows; i++)
{
    for (var j = 0; j < columns; j++)
    {
        matrix[i, j] = random.Next(-256, 257);
        Console.Write(matrix[i, j] + " ");
    }

    Console.WriteLine();
}

// Вычисление номера первого столбца, содержащего хотя бы один нулевой элемент
var columnWithZero = -1;
for (var i = 0; i < rows; i++)
{
    for (var j = 0; j < columns; j++)
    {
        if (matrix[i, j] == 0)
        {
            columnWithZero = j;
            break;
        }
    }

    if (columnWithZero != -1)
    {
        break;
    }
}

// Расчет характеристик для каждой строки (сумма отрицательных четных элементов)
var characteristics = new int[rows];
for (var i = 0; i < rows; i++)
{
    var sum = 0;
    for (var j = 0; j < columns; j++)
    {
        if (matrix[i, j] < 0 && matrix[i, j] % 2 == 0)
        {
            sum += matrix[i, j];
        }
    }

    characteristics[i] = sum;
}

Console.WriteLine("Сумма четных отрицательных значений строк");
foreach (var characteristic in characteristics)
{
    Console.Write(characteristic + " ");
}
Console.WriteLine();

// получения массива с очерёдностью строк
int temp = 0;
for (int i = 0; i < rows; i++)
{
    foreach (var q in characteristics)
    {
        if (q < temp)
            temp = q;
    }

    temp = Array.IndexOf(characteristics, temp);
    characteristics[temp] = i;
}
Console.WriteLine("Очерёдность строк в порядке убывания");
foreach (var characteristic in characteristics)
{
    Console.Write(characteristic + " ");
}
Console.WriteLine();

// заполнение нового массива строками
var newArr = new int[rows, columns];
for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        newArr[i, j] = matrix[characteristics[i], j];
    }
}


// Вывод результатов
Console.WriteLine("Матрица:");
for (var i = 0; i < rows; i++)
{
    for (var j = 0; j < columns; j++)
    {
        Console.Write(newArr[i, j] + " ");
    }

    Console.WriteLine();
}

Console.WriteLine($"Номер первого столбца, содержащего нулевой элемент: {columnWithZero}");