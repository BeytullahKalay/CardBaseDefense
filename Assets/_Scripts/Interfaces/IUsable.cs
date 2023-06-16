
public interface IUsable
{
    public bool Usable { get; set; }
    public void SetMouseBusyStateTo(bool state);
    public void OpenActions();
    public void DestroyActions();
}
