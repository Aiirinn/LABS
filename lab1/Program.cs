// ввод числа x
Console.Write("Введите число Х: ");
var isValid = int.TryParse(Console.ReadLine(), out var x); 
if (!isValid)
{
    Console.WriteLine("Некорректный ввод для числа X");
    return;
}

// ввод размера массива n
Console.Write("Введите размер массива N: ");
isValid = int.TryParse(Console.ReadLine(), out var n);
if (!isValid || n <= 0)
{
    Console.WriteLine("Размер массива должен быть целым положительным числом");
    return;
}

var array = new int[n]; 
var random = new Random(); 

// заполнение массива
for (var i = 0; i < n; i++)
{
    array[i] = random.Next(-256, 257);
    Console.Write(array[i] + " ");
}
Console.WriteLine();

var countGreaterThanX = 0;
var maxAbsValue = 0;
var maxAbsIndex = 0;
for (var i = 0; i < n; i++)
{
    if (array[i] > x) // подсчет элементов, больших, чем х
        countGreaterThanX++;
    if (Math.Abs(array[i]) > maxAbsValue) // вычисление максимального числа по модулю и его индекса
    {
        maxAbsValue = Math.Abs(array[i]);
        maxAbsIndex = i;
    }
}

double product = 1;
for (var i = maxAbsIndex + 1; i < n; i++) // вычисление произведения
{
    product *= array[i];
}

Array.Sort(array);

Console.WriteLine($"Количество элементов больших {x}: {countGreaterThanX}");
Console.WriteLine($"Произведение элементов после максимального по модулю элемента: {product}");
Console.WriteLine("Преобразованный массив:");
foreach (var num in array)
{
    Console.Write(num + " ");
}
