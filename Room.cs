using System;
using System.Collections.Generic;

namespace SmartHome
{
    public class Room
    {
        private string _name;
        private List<Device> _devices;   // композиция: Room создаёт устройства сам
        private Scenario _scenario;      // агрегация: сценарий приходит извне

        public string Name => _name;
        public int DeviceCount => _devices.Count;

        public Room(string name)
        {
            _name = name;
            _devices = new List<Device>();
            _scenario = null;

            // КОМПОЗИЦИЯ: комната сама создаёт свои устройства.
            // Снаружи их никто не создавал — они целиком принадлежат комнате.
            _devices.Add(new Device("Умная лампа", 10));
            _devices.Add(new Device("Телевизор", 150));
            _devices.Add(new Device("Умная розетка", 0));

            Console.WriteLine($"[Room] Создана комната: {_name} (устройств: {_devices.Count})");
        }

        ~Room()
        {
            Console.WriteLine($"[Room] Финализатор: комната {_name} уничтожена.");
        }

        // Добавить ещё одно устройство (опционально — для расширения).
        public void AddDevice(Device device)
        {
            _devices.Add(device);
            Console.WriteLine($"В комнату '{_name}' добавлено устройство: {device.Name}");
        }

        public void TurnOnAllDevices()
        {
            Console.WriteLine($"Включение всех устройств в комнате '{_name}':");
            foreach (var dev in _devices)
            {
                dev.TurnOn();
            }
        }

        public void TurnOffAllDevices()
        {
            Console.WriteLine($"Выключение всех устройств в комнате '{_name}':");
            foreach (var dev in _devices)
            {
                dev.TurnOff();
            }
        }

        // АГРЕГАЦИЯ: сценарий создан снаружи, мы лишь сохраняем ссылку.
        public void SetScenario(Scenario scenario)
        {
            _scenario = scenario;
            if (_scenario != null)
            {
                Console.WriteLine($"Комната '{_name}' теперь использует сценарий: {_scenario.Name}");
            }
            else
            {
                Console.WriteLine($"Комната '{_name}' отвязана от сценария.");
            }
        }

        public void RunScenario()
        {
            if (_scenario == null)
            {
                Console.WriteLine($"  Ошибка: У комнаты '{_name}' нет привязанного сценария!");
                return;
            }

            if (!_scenario.IsActive)
            {
                Console.WriteLine($"  Ошибка: Сценарий '{_scenario.Name}' не активирован.");
                return;
            }

            Console.WriteLine($"Комната '{_name}' выполняет сценарий '{_scenario.Name}'...");
            TurnOnAllDevices();
        }

        public override string ToString()
        {
            return $"Комната '{_name}', устройств: {_devices.Count}";
        }
    }
}