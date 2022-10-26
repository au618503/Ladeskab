namespace Cabinet_Library.ObserverPattern;

public interface IPublisher<T>
{
    void AddListener(IObserver observer, EventHandler<T> callback);
    void RemoveListener(EventHandler<T> callback);
}