namespace FileChunksHashViewer.Services
{
    public class SynchronizationChunkBlocker : IDisposable
    {
        private readonly bool _lockTaken;
        private SpinLock _spinLock;
        
        public SynchronizationChunkBlocker(SpinLock spinLock)
        {
            _spinLock = spinLock;

            _lockTaken = false;
            _spinLock.Enter(ref _lockTaken);
        }

        public void Dispose()
        {            
            if (_lockTaken)
            {
                _spinLock.Exit();
            }
        }
    }
}