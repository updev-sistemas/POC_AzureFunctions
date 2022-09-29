namespace ValuesObjects
{
    public class State
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Uf { get; set; }
        public virtual IEnumerable<City>? Cities { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}