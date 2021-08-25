  select top(4) *
  from dbo.Item as a
  join dbo.ItemClass as b
  on a.Class = b.Class