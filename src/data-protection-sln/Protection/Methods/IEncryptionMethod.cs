namespace Protection.Methods
{
    public interface IEncryptionMethod<TIn, TOut>
    {
        TOut Encrypt(TIn data);
        TIn Decrypt(TOut data);
    }
}