using UnityEngine;
using UnityEditor;
using Enemy.Model;
using Interfaces;
using ScriptableObjects;
using Interfaces.ServiecesInterface;
using ObjectPooling;

namespace Enemy.Controller
{
    public class ControllerEnemy : IBasePlayerController, IPoolableEnemy
    {
        ModelEnemy model;
        ViewEnemy view;
        EnemyStateMachine stateMachine;
        public ControllerEnemy()
        {
            
        }
        
        public void Set(ScriptableEnemy enemyTemp, Vector3 position, int type)
        {
            GetModel(enemyTemp);
            GetView(position);
            model.enemytype = type;
            view.gameObject.SetActive(true);
            stateMachine = new EnemyStateMachine(this);
        }
        public void Set(ScriptableEnemy enemyTemp, int type)
        {
            GetModel(enemyTemp);
            GetView(model.GetRandomSpawnPoint());
            model.enemytype = type;
            view.gameObject.SetActive(true);
            stateMachine = new EnemyStateMachine(this);
        }
        public void StateChangeNotify(EnemyState state)
        {
            stateMachine.Notify(state);

        }
        public Vector3 GetEnemyPosition()
        {
            return view.GetPosition();
        }

        public void ActivateState(EnemyState state, Vector3 position)
        {
            view.ActivateState(state, position);
            //Debug.Log(state+" Activated");
        }
        public void DeactivateState(EnemyState state)
        {
            view.DeactivateState(state);
            //Debug.Log(state+"DeActivated");
        }
        public virtual void GetModel(ScriptableEnemy enemyTemp)
        {
            model = new ModelEnemy(enemyTemp);
        }
        public virtual void GetView(Vector3 position)
        {
            view = GameObject.Instantiate(model.enemyObject.enemyPrefab, position, Quaternion.identity, null).GetComponent<ViewEnemy>();
            view.SetColour(model.enemyObject.color);
            view.controller = this;
        }
        public void BulletHit(int damage, ControllerPlayer player)
        {
            model.TakeDamage(damage);
            // Debug.Log(model.IsAlive());
            if (!model.IsAlive())
            {
                player.EnemyKilled(model.GetScore());
                DestroyObject();
            }
        }

        public int GetScore()
        {
            return model.GetScore();
        }
        public void SetAlert(Vector3 playerPosition)
        {
            ServiceLocator.Instance.get<IServiceEnemy>().SetAlert(playerPosition);
        }
        public void DestroyObject()
        {
            //ServiceEnemy.Instance.OnAlert-=OnAlert;
            stateMachine.DestroyMachine();
            stateMachine = null;
            ServiceLocator.Instance.get<IServiceEnemy>().RemoveEnemy(this);

        }
        public void Reset()
        {
            view.DestroyEnemy();
        }


        public bool IsFreez()
        {
            return model.freez; ;
        }
        public int GetEnemyType()
        {
            return model.enemytype;
        }

        public Controls GetControls()
        {
            throw new System.NotImplementedException();
        }

        public void StartBoost()
        {
            throw new System.NotImplementedException();
        }

        public void StopBoost()
        {
            throw new System.NotImplementedException();
        }

        public void Update(float horizontal, float vertical)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateScore(int score)
        {
            throw new System.NotImplementedException();
        }

        public void Move(float horizontal, float vertical)
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }
    }
}
