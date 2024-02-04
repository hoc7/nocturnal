namespace BoneGame.Nocturnal.Planetarium.Book
{
    /// <summary>
    /// 文字タブを選択したメッセージ
    /// </summary>
    public class CharacterTabSelectMessage
    {
        public CharacterTab Tab { get; private set; }

        public CharacterTabSelectMessage(CharacterTab tab)
        {
            Tab = tab;
        }
    }

    /// <summary>
    /// 種別タブを選択したメッセージ
    /// </summary>
    public class TypeTabSelectMessage
    {
        public TypeTab Tab { get; private set; }

        public TypeTabSelectMessage(TypeTab tab)
        {
            Tab = tab;
        }
    }

    public enum TypeTab
    {
        Sign,
        Star
    }

    public enum CharacterTab
    {
        あ,
        か,
        さ,
        た,
        な,
        は,
        ま,
        や,
        ら,
        わ,
    }
}