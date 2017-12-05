import React, { Component } from 'react';
import {
  Card,
  CardHeader,
  CardBody,
  ButtonDropdown,
  DropdownToggle
} from 'reactstrap';
import classnames from 'classnames';

export default class StandardPanel extends Component {
  constructor(props) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.fullScreen = this.fullScreen.bind(this);
    this.state = {
      dropdownOpen: false,
      fullScreen: false
    };
  }

  toggle() {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen
    });
  }

  fullScreen() {
    this.setState({
      fullScreen: !this.state.fullScreen
    });
  }

  render() {
    return (
      <Card
        className={classnames(
          { 'b-panel-fullscreen': this.state.fullScreen },
          'b-panel'
        )}
      >
        <CardHeader>
          <h3 className="b-panel-title">
            <i className="icon-notebook b-icon" />
            {this.props.title || 'No title'}
          </h3>

          {this.props.actions && (
            <span className="b-panel-actions">
              <ButtonDropdown
                isOpen={this.state.dropdownOpen}
                toggle={this.toggle}
              >
                <DropdownToggle caret color="primary" size="sm">
                  Actions
                </DropdownToggle>

                {this.props.actions || ''}
              </ButtonDropdown>&nbsp;&nbsp;
              <i
                className={classnames(
                  { 'icon-size-fullscreen': !this.state.fullScreen },
                  { 'icon-size-actual': this.state.fullScreen },
                  'b-icon'
                )}
                onClick={this.fullScreen}
              />{' '}
            </span>
          )}
        </CardHeader>

        <CardBody className="card-body">{this.props.children}</CardBody>
      </Card>
    );
  }
}
