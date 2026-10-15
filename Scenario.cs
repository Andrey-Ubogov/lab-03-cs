using System;

namespace SmartHome
{
    /// <summary>
    /// —ценарий автоматизации умного дома.
    /// »спользуетс€ комнатой (агрегаци€) Ч создаЄтс€ снаружи.
    /// </summary>
    public class Scenario
    {
        private string _name;
        private string _condition;
        private bool _isActive;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Condition
        {
            get => _condition;
            private set => _condition = value;
        }

        public bool IsActive
        {
            get => _isActive;
            private set => _isActive = value;
        }

        public Scenario(string name, string condition)
        {
            _name = name;
            _condition = condition;
            _isActive = false;
            Console.WriteLine($"[Scenario] —оздан сценарий: {_name} ({_condition})");
        }

        ~Scenario()
        {
            Console.WriteLine($"[Scenario] ‘инализатор: сценарий {_name} уничтожен.");
        }

        // —одержательные методы

        public void Activate()
        {
            _isActive = true;
            Console.WriteLine($"  —ценарий '{_name}' активирован.");
        }

        public void Deactivate()
        {
            _isActive = false;
            Console.WriteLine($"  —ценарий '{_name}' деактивирован.");
        }

        public void ChangeCondition(string newCondition)
        {
            _condition = newCondition;
            Console.WriteLine($"  ” сценари€ '{_name}' изменено условие на: {_condition}");
        }

        public override string ToString()
        {
            string state = _isActive ? "активен" : "не активен";
            return $"—ценарий '{_name}' ({_condition}) Ч {state}";
        }
    }
}