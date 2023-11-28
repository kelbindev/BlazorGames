using LogicLayer.MineSweeper.Enum;

namespace LogicLayer.MineSweeper;
public class Cell
{
    public int Value = 0;
    public bool Revealed = false;
    public string RevealedString => Revealed ? "revealed" : "";
    public string Danger => Revealed && Value is not 0 ? "danger" : "";
    public string Safe => Revealed && Value is 0 ? "safe" : "";
    public string Mine => Revealed && Value == (int)_Mine.mine ? "mine" : "";
    public string Display => !Revealed || Value is 0 ? " " : Value == (int)_Mine.mine ? "X" : Value.ToString();
}
