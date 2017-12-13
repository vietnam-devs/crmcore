import React, { PureComponent } from 'react';
import moment from 'moment';

import { ListGroup, ListGroupItem, Badge } from 'reactstrap';

import { StandardPanel } from 'components';

import styled from 'styled-components';
const TaskItemColorContent = styled.div`
  color: #000;
`;

export class NotStartedPanel extends PureComponent {
  render() {
    return (
      <TaskStatusPanel
        statusTitle="Not Started"
        statusType="bg-warning"
        {...this.props}
      />
    );
  }
}

export class InProgressPanel extends PureComponent {
  render() {
    return (
      <TaskStatusPanel
        statusTitle="In Progress"
        statusType="bg-info"
        {...this.props}
      />
    );
  }
}

export class PendingPanel extends PureComponent {
  render() {
    return (
      <TaskStatusPanel
        statusTitle="Pending"
        statusType="bg-danger"
        {...this.props}
      />
    );
  }
}

export class DonePanel extends PureComponent {
  render() {
    return (
      <TaskStatusPanel
        statusTitle="Done"
        statusType="bg-success"
        {...this.props}
      />
    );
  }
}

class TaskStatusPanel extends PureComponent {
  render() {
    const { tasks, taskByKeys, categoryByKeys, loading } = this.props;
    let taskComponents = [];

    if (tasks !== null && tasks.length > 0 && categoryByKeys != null) {
      taskComponents = tasks.map(key => {
        const task = taskByKeys[key];
        let badge;

        if (categoryByKeys[task.categoryType] !== undefined) {
          badge = (
            <Badge pill className="pull-left">
              {categoryByKeys[task.categoryType].value}
            </Badge>
          );
        }

        return (
          <ListGroupItem key={key} className="justify-content-between">
            {badge}
            &nbsp;<a href="#">{task.assignedTo}</a>: {task.name} -{' '}
            {moment(task.lastUpdated).format('MMMM Do YYYY, h:mm:ss A')}
          </ListGroupItem>
        );
      });
    }

    return (
      <StandardPanel
        className={this.props.statusType}
        title={this.props.statusTitle}
        showMaximize={true}
        showIcon={false}
      >
        <TaskItemColorContent>
          {loading && (
            <div className="text-center">
              <i className="fa fa-spinner fa-pulse fa-3x fa-fw" />
            </div>
          )}
          {!loading && (
            <ListGroup>
              {taskComponents}
              <ListGroupItem>
                <div className="text-center">
                  {(tasks === null || tasks.length <= 0) && (
                    <span>No Task.</span>
                  )}
                  {tasks !== null &&
                    tasks.length > 0 && <a href="#">See All...</a>}
                </div>
              </ListGroupItem>
            </ListGroup>
          )}
        </TaskItemColorContent>
      </StandardPanel>
    );
  }
}
