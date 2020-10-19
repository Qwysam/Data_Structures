#include <iostream>
#include <string>
using namespace std;

typedef struct node {
   int val;
   struct node *next;
} node_t;

void enqueue(node_t **head, int val) {
   node_t *new_node = (node_t*)malloc(sizeof(node_t));
   if (!new_node) 
    return;
   new_node->val = val;
   new_node->next = *head;

   *head = new_node;
}

int dequeue(node_t **head) {
    node_t *current, *prev = NULL;
    int retval = -1;
    if (*head == NULL) 
        return -1;
    current = *head;
     while (current->next != NULL) {
        prev = current;
        current = current->next;
   }
    retval = current->val;
    free(current);

   if (prev)
      prev->next = NULL;
   else
      *head = NULL;

   return retval;
}

void delete_queue(node_t *head) {
    head -> val = NULL;
    head -> next = NULL;
}

int count_queue(node_t *head){
    int count = 1;
    node_t * temp = head;
    if(temp -> val == NULL)
        count = 0;
    while(temp->next!= NULL){
        count++;
        temp = temp->next;
    }
    return count;
}

bool queue_is_empty(node_t *head){
    if(head->val==NULL&&head->next==NULL)
        return true;
    else
        return false;
}

void print_queue(node_t *head) {
    node_t *current = head;

    while (current != NULL) {
        printf("%d ", current->val);
        current = current->next;
    }
    cout << endl;
}

struct Stack // структура элемента для формирования стека
{   
    int value;       // информационная часть элемента стека
	Stack * next;   // указатель на элемент стека
};

typedef Stack * Link; //Link - указатель на структуру типа Stack

Link newNode() // создает элемент для стека и возвращает указатель на него
{   int n;
	cout<<"Input element: ";
	scanf("%d", &n);
    Link v = new Stack;
    v -> value = n;
    v -> next = NULL;
    return v;
}

Link insert(Link top, Link t)                 // вставить t в стек после top
{ 
    t->next = top;          
    top = t;
    return top;	  
}

Link pop_stack(Link top)                //удаление элемента из вершины стека
{if(top==NULL)        //если top =NULL - стек пуст
	{ 	cout<<"Stack is empty!"<<endl;
		return top;
	}
		else
		{	   
	        Link t = top;
	        top=t->next;
	        delete(t);
            return top;
		}
}

void print_top_stack(Link t)               //печать элемента из вершины стека
{
    if(t==NULL) 
	{
        cout<<"Stack is empty!"<<endl;
	}
	else	
	{   
        cout<<"top->value="<<t->value<<endl;
	}
}

bool stack_is_empty(Link t)                     //определение - пуст ли стек
{ 
    if(t==NULL) 
        return true;		
    else 
        return false;
}

Link delete_stack(Link top)                 //уничтожение стека
{
    if(top==NULL) 
	{ 	
        cout<<"Stack is empty!"<<endl;
		return top;
	}
	else
	{
        while(top != NULL) // условие можно написать проще - (t)
        {   
            Link t = top;
            top = t->next;
            delete(t);
        }
        return top;
	}
}

int count_stack(Link top) //определение числа элементов в стеке
{
	if(top==NULL) 
	{ 	
        cout<<"Stack is empty!"<<endl;
		return -1;
	}
	else	
	{        
        Link t = top;
        int i=0;
     	while(t != NULL)     // условие можно написать проще - (t)
		{ 
            t = t->next;
		    i++;
		}
		cout<<"Number of stack elements equal - "<<i<<endl;
		return i;
	}
}

void print_stack(Link top)                        // печать элементов стека
{ 
    Link t = top;
    while(t != NULL)             // условие можно написать проще - (t)
    { 
      cout << t->value << " ";
        t = t->next;
    }
    cout << endl;
}

int main(void)
{
    char option, action;
    int count = 0, max;
    node_t *head = NULL;
    Link temp,top;
    for(;;){
        if(count == 0){
            cout << "Input number of elements in data structure: ";
            cin >> max;
            cout << "Input 's' to create stack, or 'q' to create queue: ";
            cin >> option;
            if(option == 's'){
            top = newNode();
            count++;
            }
            if(option == 'q'){
            cout << "Input first element: ";
            int i;
            cin >> i;
            enqueue(&head,i);
            count++;
            }
        }
        if(count>0 && count <=max){
            cout << "Actions:\n";
            cout << "'d' to delete structure\n'i' to input element\n'r' to remove element\n";
            cout << "'c' to count number of elements\n'm' to check if structure is empty\n'p' to print elements\n";
            cout << "'e' to exit\n";
            cout << "Input action: ";
            cin >> action;
            if(action=='e')
                break;
            if(action == 'd'){
                if(option == 's')
                    top = delete_stack(top);
                else
                    delete_queue(head);
                count = 0;
            }
            if(action == 'i'){
                if(count == max)
                    cout << "Too many elements\n";
                else{
                if(option == 's'){
                    temp = newNode();
                    top = insert(top,temp);
                }
                else{
                    int i;
                    cout << "Input element: ";
                    cin >> i;
                    enqueue(&head,i);
                }
                count++;
                }
            }
            if(action == 'r'){
                if(option == 's')
                    top = pop_stack(top);
                else
                    dequeue(&head);
                count--;
            }
            if(action == 'c'){
                int c;
                if(option == 's'){
                    c = count_stack(top);
                }
                else{
                    c = count_queue(head);
                }
                cout << c << " elements in structure\n";
            }
            if(action == 'm'){
                if(option == 's'){
                    if(stack_is_empty(top))
                        cout << "Stack is empty\n"; 
                    else
                        cout << "Stack is not empty\n";
                }
                else{
                    if(queue_is_empty(head))
                        cout << "Queue is empty\n";
                    else
                        cout << "Queue is not empty\n";
                }
            }
            if(action == 'p'){
                if(option == 's')
                    print_stack(top);
                else
                    print_queue(head);
            }
        } 
    }
    return 0;
}
