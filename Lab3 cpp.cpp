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
        printf("%d\n", current->val);
        current = current->next;
    }
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
{    t->next = top;          
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
            cout<<"poping top->value="<<top->value<<endl;
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
		cout<<"You do stack empty! top = = NULL"<<endl;
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
    char option;
    cout << "Input 's' to create stack, or 'q' to create queue: ";
    cin >> option; 
    if(option == 's'){
        Link l = newNode();
        Link k = newNode();
        insert(k,l);
        print_stack(l);
    }
    if(option == 'q'){
        node_t *head = NULL;
        cout << "Input first element: ";
        int i;
        cin >> i;
        enqueue(&head,i);
        enqueue(&head,i);
        delete_queue(head);
        cout << "There are " << count_queue(head) << " elements\n";
    }
    return 0;
}
