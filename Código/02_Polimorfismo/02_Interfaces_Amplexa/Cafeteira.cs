namespace POO_C__Interfaces_Amplexa {
    internal class Cafeteira : Dispositivo {
        
        public Cafeteira(string nome) : base(nome) {
            Desligar();
        }

        public override bool Ligar() {
            _ligado = true;
            SetEstado("Cafeteira ligada.");
            return _ligado;
        }

        public override bool Desligar() {
            _ligado = false;
            SetEstado("Cafeteira desligada.");
            return _ligado;
        }
    }
}