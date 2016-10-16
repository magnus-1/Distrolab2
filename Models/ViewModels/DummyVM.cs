
namespace community.Models.ViewModels {
    public class DummyVM 
    {
        public string Name { get; set; }
        public string MyData { get; set; }
        public string SomeDate { get; set; }

        public override string ToString() {
            return "DummyVM: " + Name + " : " + MyData + " : "+ SomeDate ;
        }
    }

}