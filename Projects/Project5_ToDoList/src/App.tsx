import { useState } from 'react'
import './App.css'
import { Button, Card, Divider, Grid, Header, Icon, Input, Segment } from 'semantic-ui-react'

export type toDoDto = {
  id:string,
  task:string,
  isCompleted:boolean,
  createdDate:Date,
};

function App() {

  const generateUniqueId = () => {
    return Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
  };

  const componentStyles = {
    myHeader:{
      cursor:"pointer",
      color:"#1DBFBA"
    },

    myInput:{
      color:"grey",
      labelPosition: "right",
      icon: "plus",
      content: "Add",
    }
  }

  const [addedToDos, setAddedToDos] = useState<toDoDto[]>([]);

  const [toDo, setToDo] =useState<toDoDto>({
    id: '',
    task: '',
    isCompleted: false,
    createdDate: new Date(),
  });

  const [sortOrder, setSortOrder] = useState<'ascending' | 'descending'>('ascending');

  const handleAddTodo = () => {

    const sameToDo = addedToDos.find(x => x.task===toDo.task);

    if(!sameToDo){
      setAddedToDos([...addedToDos,toDo]);
      setToDo({
        id: generateUniqueId(),
        task: '',
        isCompleted: false,
        createdDate: new Date(),
      });

    }
  }

  const handleAddedTodoDelete = (selectedToDo:string) => {

    const newAddedToDos = addedToDos.filter((toDos) => toDos.task !== selectedToDo);

    setAddedToDos(newAddedToDos);

  }

  const sortedToDos = addedToDos.slice().sort((a, b) => {
    const dateA = new Date(a.createdDate);
    const dateB = new Date(b.createdDate);
    
    if (sortOrder === 'ascending') {
      return dateA.getTime() - dateB.getTime();
    } else {
      return dateB.getTime() - dateA.getTime();
    }
  });

  const toggleSortOrder = () => {
    setSortOrder((prevSortOrder) =>
      prevSortOrder === 'ascending' ? 'descending' : 'ascending'
    );
  };

  return (
    <>
      <Segment textAlign='center'> 
        <Grid columns={2} relaxed='very' stackable>
          <Grid.Column width={13}>
            <Input action={{...componentStyles.myInput, onClick:handleAddTodo, disabled: toDo.task.trim()===''}}
              style={{ width: '100%' }} 
              placeholder="New ToDo..." 
              value={toDo.task} 
              onChange={(e) => setToDo({...toDo, task:e.target.value})}/>
          </Grid.Column>
          <Grid.Column width={3} verticalAlign='middle'>
            <Icon size ="big"
                  name={sortOrder === 'ascending' ? 'sort numeric ascending' : 'sort numeric descending'}
                  onClick={toggleSortOrder}
                  style={{ cursor: 'pointer', alignSelf: 'flex-end' }}
                />
          </Grid.Column>
        </Grid>
      
        <Divider horizontal>
          <Header as='h2' textAlign="center" size="large">
            <Icon name='pencil' size="tiny" />
            <Header.Content>ToDos</Header.Content>  
          </Header>
        </Divider>
      </Segment>
            
          

          
        

        





        {/* <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Header as='h2' textAlign="center" size="huge">
            <Icon name='pencil' size="tiny" />
            <Header.Content>ToDos</Header.Content>
            <Icon
              name={sortOrder === 'ascending' ? 'sort numeric ascending' : 'sort numeric descending'}
              onClick={toggleSortOrder}
              style={{ cursor: 'pointer', alignSelf: 'flex-end' }}
            />
          </Header>
        </div> */}
      
      
      <br></br>
      <div>
        <Card.Group style={{ width: '700px' }}>
          {sortedToDos.map((toDo) => (
            <Card fluid color="blue" key={toDo.id}>
              <Card.Content>

                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                  <Card.Header 
                    style={{ textDecoration: toDo.isCompleted ? 'line-through' : 'none' }} 
                    onDoubleClick={() => {
                      const updatedToDos = addedToDos.map((item) =>
                          item.id === toDo.id ? { ...item, isCompleted: !item.isCompleted } : item
                        );
                        setAddedToDos(updatedToDos);
                    }}>               
                    {toDo.task}
                  </Card.Header>
                    <Card.Description>
                      <Button icon onClick={() => handleAddedTodoDelete(toDo.task)}><Icon name='trash' /></Button>
                    </Card.Description>
                </div>
              </Card.Content>
            </Card>
          ))}
        </Card.Group>
      </div>
      
    </>
  )
}

export default App
