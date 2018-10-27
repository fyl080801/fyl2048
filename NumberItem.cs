
namespace fyl2048
{
    public class NumberItem
    {
        public int Value { get; private set; }

        public NumberItem(int value)
        {
            Value = value;
        }

        public void Combin(NumberItem item)
        {
            Value += item.Value;
            item.Reset();
        }

        public void Swap(NumberItem item)
        {
            var val = this.Value;
            this.Value = item.Value;
            item.Value = val;
        }

        public bool IsEmpty()
        {
            return this.Value == 0;
        }

        public void Reset(int? val)
        {
            Value = val.HasValue ? val.Value : 0;
        }

        public void Reset()
        {
            Reset(null);
        }

        public bool Check(object obj)
        {
            return this.Value == ((NumberItem)obj).Value && this.Value != 0;
        }
    }
}