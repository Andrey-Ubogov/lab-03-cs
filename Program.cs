using System;
using System.Text;

namespace SmartHome
{
    /// <summary>
    /// Вспомогательный класс: журнал событий умного дома.
    /// Реализует IDisposable, чтобы освобождать ресурс через using.
    /// </summary>
    public class IssueLog : IDisposable
    {
        private readonly string _title;
        private bool _isOpen;

        public IssueLog(string title)
        {
            _title = title;
            _isOpen = true;
            Console.WriteLine($"[+] Журнал открыт: {_title}");
        }

        public void Write(string line)
        {
            if (!_isOpen)
            {
                Console.WriteLine("  Ошибка: журнал уже закрыт!");
                return;
            }
            Console.WriteLine($"  {line}");
        }

        // Dispose вызывается гарантированно при выходе из блока using.
        public void Dispose()
        {
            if (_isOpen)
            {
                _isOpen = false;
                Console.WriteLine($"[-] Журнал закрыт: {_title}");
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            // Русские буквы в консоли Windows
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("========== ЛР 3. Часть 1: C# ==========\n");

            // ==========================================
            // ДЕМОНСТРАЦИЯ 1: ССЫЛКИ И КОПИИ
            // ==========================================
            Console.WriteLine("--- 1. Ссылки и копии ---");

            Scenario morning = new Scenario("Утренний свет", "07:00");
            morning.Activate();

            // Присваивание копирует ССЫЛКУ, а не объект.
            Scenario copy = morning;

            // Изменим через copy — изменится и morning, потому что это ОДИН объект.
            copy.ChangeCondition("08:00");

            Console.WriteLine($"morning.Condition = {morning.Condition}");
            Console.WriteLine($"copy.Condition    = {copy.Condition}");
            Console.WriteLine($"morning == copy?  {ReferenceEquals(morning, copy)}");

            Console.WriteLine();

            // ==========================================
            // ДЕМОНСТРАЦИЯ 2: ОСВОБОЖДЕНИЕ РЕСУРСА
            // ==========================================
            Console.WriteLine("--- 2. Освобождение ресурса (using) ---");

            using (IssueLog log = new IssueLog("Абонемент умного дома"))
            {
                log.Write("Комната 'Гостиная' активирована.");
                log.Write("Сценарий 'Утренний свет' запущен.");
            } // Здесь ГАРАНТИРОВАННО вызывается Dispose() — ресурс освобождён.

            Console.WriteLine();

            // ==========================================
            // ДЕМОНСТРАЦИЯ 3: СБОРЩИК МУСОРА
            // ==========================================
            Console.WriteLine("--- 3. Сборщик мусора ---");

            WeakReference<Device> weakDevice = CreateTemporaryDevice();

            Console.WriteLine($"Объект жив до сборки? {weakDevice.TryGetTarget(out _)}");

            // Просим сборщик мусора поработать (это всего лишь просьба!).
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine($"Объект жив после сборки? {weakDevice.TryGetTarget(out _)}");

            Console.WriteLine();

            // ==========================================
            // ОСНОВНОЙ СЦЕНАРИЙ: КОМПОЗИЦИЯ И АГРЕГАЦИЯ
            // ==========================================
            Console.WriteLine("--- 4. Композиция и агрегация ---");

            Room livingRoom = new Room("Гостиная");
            livingRoom.SetScenario(morning);
            livingRoom.RunScenario();

            Console.WriteLine("\n--- Проверка правила: поломка устройства ---");
            // Чтобы сломать устройство внутри комнаты, нужно получить к нему доступ.
            // В нашей модели такого API нет — поэтому создадим отдельное устройство.
            Device brokenLamp = new Device("Сломанная лампа", 5);
            brokenLamp.Break();
            brokenLamp.TurnOn();

            Console.WriteLine("\n--- Агрегация: сценарий жив, хотя комната уже не нужна ---");
            livingRoom = null; // Отпускаем комнату. Но сценарий morning всё ещё жив.
            Console.WriteLine($"Сценарий '{morning.Name}' жив: {morning != null}");
            Console.WriteLine(morning);

            Console.WriteLine("\n========== КОНЕЦ ==========");
        }

        /// <summary>
        /// Создаёт временное устройство и возвращает слабую ссылку на него.
        /// Локальная переменная temp исчезает при выходе из метода —
        /// значит, на объект больше нет обычных ссылок.
        /// </summary>
        private static WeakReference<Device> CreateTemporaryDevice()
        {
            Device temp = new Device("Временное устройство", 1);
            return new WeakReference<Device>(temp);
        }
    }
}